using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] float healAmount = 80f;
    [SerializeField] float rotateSpeed = 5f;
    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                FindObjectOfType<PlayerHealth>().Heal(healAmount);
                Destroy(gameObject);
            }
        }

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
