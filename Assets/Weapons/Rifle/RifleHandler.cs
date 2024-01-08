using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RifleHandler : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 25f;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private AudioSource fireSFX;
    [SerializeField] private TextMeshProUGUI displayAmmo;
    private int currentAmmo;
    bool canShoot = true;
    void Start()
    {
        ammoSlot = GetComponentInParent<Ammo>();
    }

    private void OnEnable()
    {
        canShoot = true;
        UpdateDisplay();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);

        if(currentAmmo > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            fireSFX.Play();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            UpdateDisplay();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }

    void UpdateDisplay()
    {
        currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        displayAmmo.text = ammoType + ": " + currentAmmo;
    }
}
