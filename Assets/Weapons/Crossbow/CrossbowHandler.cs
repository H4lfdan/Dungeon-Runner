using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrossbowHandler : MonoBehaviour
{
Animator animator;

[SerializeField] private float reloadTime;
[SerializeField] private float fireTime;
[SerializeField] private float firepower;
[SerializeField] GameObject arrowPrefab;
[SerializeField] private Transform spawnPoint;
[SerializeField] Ammo ammoSlot;
[SerializeField] AmmoType ammoType;
[SerializeField] private AudioSource fireSFX;
[SerializeField] private TextMeshProUGUI displayAmmo;
private GameObject currentArrow;
private int currentAmmo;

private bool isReloading;

    void Start()
    {
        animator = GetComponent<Animator>();
        ammoSlot = GetComponentInParent<Ammo>();
        UpdateDisplay();
    }

    void OnEnable()
    {
        UpdateDisplay();
        if(isReloading && currentArrow == null)
        {
            StartCoroutine(ReloadAfterTime());
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!isReloading && currentArrow == null)
            {
                Reload();
            }

            if(!isReloading && currentArrow != null)
            {
                Shoot();
            }
        }
    }

    public void Reload()
    {
        currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);

        if(isReloading || currentArrow != null || currentAmmo <= 0) return;
        isReloading = true;
        animator.Play("Fill");
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
        currentArrow.transform.localPosition = Vector3.zero;
        isReloading = false;
    }

    private void Shoot()
    {
        Debug.Log("Fired an arrow!");
        
        if(isReloading || currentArrow == null) return;
        {
            animator.Play("Shoot");
            fireSFX.Play();
            var force = spawnPoint.TransformDirection(Vector3.forward * firepower);
            currentArrow.GetComponent<Projectile>().Fly(force);
            new WaitForSeconds(fireTime);
            currentArrow = null;
            ammoSlot.ReduceCurrentAmmo(ammoType);
            //UpdateDisplay();
        }
    }

    public bool IsReady()
    {
        return (!isReloading && currentArrow != null);
    }

    void UpdateDisplay()
    {
        currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        displayAmmo.text = ammoType + ": " + currentAmmo;
    }
}
