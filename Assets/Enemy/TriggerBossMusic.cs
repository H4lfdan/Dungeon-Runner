using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossMusic : MonoBehaviour
{
    EnemyAI enemyAI;
    [SerializeField] private AudioSource bossMusic;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    void Update()
    {
        if(bossMusic.isPlaying) return;

        if(enemyAI.isProvoked)
        {
            bossMusic.Play();
        }
    }
}
