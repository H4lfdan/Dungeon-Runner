using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 40f;
    [SerializeField] private AudioSource attackSFX;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        attackSFX.Play();
        if (target == null) return;
        Debug.Log("You got bit!");
        target.TakeDamage(damage);
    }
}
