using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderSeeker : MonoBehaviour
{
    private Transform target;
    private PlayerHealth targetHealth;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float firepower = 20f;
    [SerializeField] private float damage = 40f;
    [SerializeField] private GameObject hitFX;
    void Start()
    {
        target = FindObjectOfType<Camera>().transform;
        targetHealth = FindObjectOfType<PlayerHealth>();
        transform.SetParent(null);
    }

    void Update()
    {
        transform.LookAt(target);
        transform.position += transform.forward * Time.deltaTime * firepower;
    }

    void OnTriggerEnter(Collider other)
    {
        //if(didHit) return;
        //didHit = true;

        if(other.gameObject.tag == "Player")
        {
            Debug.Log( "hit" + GetComponent<Collider>().name);
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        Instantiate(hitFX, transform.position, Quaternion.identity);
        if (other.gameObject.tag == "Destructable")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject, .01f);
    }
}
