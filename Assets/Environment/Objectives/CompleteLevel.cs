using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private Canvas victoryCanvas;
    // Start is called before the first frame update
    void Start()
    {
        victoryCanvas.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            {
                victoryCanvas.enabled = true;
                Time.timeScale = 0;
                FindObjectOfType<WeaponSwitcher>().enabled = false;
                FindObjectOfType<StarterAssets.FirstPersonController>().enabled = false;
                FindObjectOfType<MenuHandler>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
    }
}
