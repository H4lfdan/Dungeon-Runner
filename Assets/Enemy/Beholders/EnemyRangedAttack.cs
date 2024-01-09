using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void RangedAttackEvent()
    {
        if (target == null) return;
        Instantiate(missilePrefab, spawnPoint);
        //Debug.Log(gameObject.name + "tried to fire a shot");
    }
}
