using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] AmmoType ammoType;
    [SerializeField] private TextMeshProUGUI displayAmmo;
    private Ammo ammo;
    private int currentAmmo;

    private void Start()
        {
            ammo = FindObjectOfType<Ammo>();
        }
    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ammo.IncreaseCurrentAmmo(ammoType, ammoAmount);
                //UpdateDisplay();
                Destroy(gameObject);
            }
        }

    private void UpdateDisplay()
        {
            currentAmmo = ammo.GetCurrentAmmo(ammoType);
            displayAmmo.text = ammoType + ": " + currentAmmo;
        }
}
