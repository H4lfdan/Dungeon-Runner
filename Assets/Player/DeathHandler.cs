using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        FindObjectOfType<StarterAssets.FirstPersonController>().enabled = false;
        FindObjectOfType<MenuHandler>().enabled = false;
        //GetComponentInChildren<CrossbowHandler>().enabled = false;
        //GetComponentInChildren<RifleHandler>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
