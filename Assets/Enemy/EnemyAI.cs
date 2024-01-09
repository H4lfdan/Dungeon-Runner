using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;
    Vector3 startingPos;
    private float distanceToStartPos;
    private Vector3 startDirection;
    EnemyAI[] allies;
    [SerializeField] private float aggroRange = 10f;
    [SerializeField] float alertAllyRange = 5f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] private float stoppingDistance = 4.75f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] private Collider hitBox;

    private float aggroTimer = 0f;
    [SerializeField] private float aggroTimeThresh = 3f;

    private float deaggroTimer = 0f;

    [SerializeField] private float deaggroTimeThresh = 3f;

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    float distanceToAlly = Mathf.Infinity;
    public bool isProvoked = false;
    public bool isAlive = true;

    [SerializeField] private AudioSource aggroSFX;
    [SerializeField] private AudioSource deathSFX;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        startingPos = transform.position;
        // Have agent face starting direction when they return to starting position
        startDirection = transform.forward;
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update()
    {
        if(isAlive)
        {
            ProcessAggression();

            if(isProvoked)
            {
                EngageTarget();
                AlertAllies();
            }
            else 
            {
                ReturnHome();
            }
        }
    }

    private void ProcessAggression()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(hasLineOfSight())
        {
            aggroTimer += Time.deltaTime;
            deaggroTimer = 0f;
        }
        else
        {
            deaggroTimer += Time.deltaTime;
            aggroTimer = 0f;
        }

        bool stillProvoked = isProvoked && deaggroTimer <= deaggroTimeThresh;
        bool aggroTimeMet = aggroTimer >= aggroTimeThresh;
        bool inRange = distanceToTarget <= aggroRange;
        isProvoked = stillProvoked || aggroTimeMet && inRange;
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void EngageTarget()
    {
        if(distanceToTarget >= attackRange)
        {
            ChaseTarget();
        }

        if(distanceToTarget <= attackRange)
        {
            FaceTarget();
            AttackTarget();
        }

        if(distanceToTarget <= stoppingDistance)
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }

    void AlertAllies()
    {
        allies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI ally in allies)
        {
            distanceToAlly = Vector3.Distance(ally.transform.position, transform.position);
            if(distanceToAlly <= alertAllyRange && ally.isAlive)
            {
                ally.isProvoked = true;
            }
        }
    }

    void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        //transform.rotation = where the target is, we need to rotate at a certain speed
    }

    private bool hasLineOfSight()
    {
        bool hasLineOfSight = false;
        Vector3 headOffset = new Vector3(0f , 2f , 0f);
        Vector3 targetLOSpos = target.position + headOffset;
        Vector3 ownLOSpos = transform.position + headOffset;
        Vector3 direction = (targetLOSpos - ownLOSpos).normalized;
        RaycastHit hit;
        Physics.Raycast(ownLOSpos , direction , out hit , Mathf.Infinity);
        hasLineOfSight = (hit.transform == target);
        return hasLineOfSight;
    }

    void ReturnHome()
    {
        GetComponent<Animator>().SetBool("attack", false);
            distanceToStartPos = Vector3.Distance(startingPos , transform.position);

            if (distanceToStartPos >= 4f)
            {
                navMeshAgent.SetDestination(startingPos);
                GetComponent<Animator>().SetTrigger("move");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Idle");
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(startDirection.x, 0, startDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
            }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.DrawWireSphere(transform.position, alertAllyRange);
    }

    public void HandleDeath()
    {
        AlertAllies();
        navMeshAgent.SetDestination(transform.position);
        navMeshAgent.enabled = false;
        deathSFX.Play();
        hitBox.enabled = false;
        isAlive = false;
    }

}
