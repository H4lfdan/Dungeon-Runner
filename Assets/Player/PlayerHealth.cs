using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float hitPoints = 120f;
    [SerializeField] TextMeshProUGUI displayHealth;

    void Awake()
    {
        UpdateDisplay();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        UpdateDisplay();
        if(hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
            Debug.Log("Ya ded, sun.");
        }
    }

    public void Heal(float healAmount)
    {
        hitPoints += healAmount;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayHealth.text = "Health:" + hitPoints;
    }
}
