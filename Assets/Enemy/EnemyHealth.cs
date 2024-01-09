using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] GameObject lootDrop;
    [SerializeField] Transform lootSpawn;
    [SerializeField] private AudioSource arrowSFX;

    public void TakeDamage(float damage)
    {
        if (hitPoints <= 0) return;
        
        BroadcastMessage("OnDamageTaken");
        
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            GetComponent<EnemyAI>().HandleDeath();
            GetComponent<EnemyAI>().enabled = false;
            GetComponent<Animator>().SetTrigger("Death");
            Instantiate(lootDrop , lootSpawn);
        }
    }

    public void ArrowImpactSFX()
    {
        arrowSFX.Play();
    }
}
