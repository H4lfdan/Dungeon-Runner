using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage = 25f;

    [SerializeField] private float torque;

    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Collider collider;

    TrailRenderer trailRenderer;

    private bool isFlying;
    private bool didHit;

    public void Fly(Vector3 force)
    {
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.isKinematic = false;
        collider.enabled = true;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        trailRenderer.enabled = true;
        rigidbody.AddForce(force, ForceMode.Impulse);
        rigidbody.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    void OnTriggerEnter(Collider collider)
    {
        //if(didHit) return;
        //didHit = true;

        Debug.Log( "hit" + collider.name);


        if(collider.CompareTag("Enemy"))
        {
            var health = collider.GetComponent<EnemyHealth>();
            health.TakeDamage(damage);
            health.ArrowImpactSFX();
            Destroy(gameObject);
        }

        Destroy(gameObject, 5);


    // Code for having arrows stick in colliders
        //rigidbody.velocity = Vector3.zero;
        //rigidbody.angularVelocity = Vector3.zero;
        //rigidbody.isKinematic = true;
        //transform.SetParent(collider.transform);
    }
}
