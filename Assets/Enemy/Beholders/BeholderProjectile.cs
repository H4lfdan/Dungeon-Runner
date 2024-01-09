using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderProjectile : MonoBehaviour
{
    private Transform target;
    private PlayerHealth targetHealth;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float firepower = 20f;
    [SerializeField] private float damage = 40f;
    void Start()
    {
        target = FindObjectOfType<Camera>().transform;
        targetHealth = FindObjectOfType<PlayerHealth>();
        transform.LookAt(target);
        transform.SetParent(null);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * firepower;
    }

    void OnTriggerEnter(Collider other)
    {
        //if(didHit) return;
        //didHit = true;

        if(other.gameObject.tag == "Player")
        {
            //var health = other.GetComponent<PlayerHealth>();
            Debug.Log( "hit" + GetComponent<Collider>().name);
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
