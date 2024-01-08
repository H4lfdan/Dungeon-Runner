using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] Canvas pauseGameCanvas;

	void Start()
	{
		pauseGameCanvas.enabled = false;
	}
    void Update()
    {
        ToggleEscapeMenu();
    }

    private void ToggleEscapeMenu()
    {
		    if (pauseGameCanvas.enabled == false && Input.GetKeyDown(KeyCode.Escape))
		    {
			    pauseGameCanvas.enabled = true;
        	    Time.timeScale = 0;
    	    	FindObjectOfType<WeaponSwitcher>().enabled = false;
        		FindObjectOfType<StarterAssets.FirstPersonController>().enabled = false;
            	//GetComponentInChildren<CrossbowHandler>().enabled = false;
                //GetComponentInChildren<RifleHandler>().enabled = false;
            	Cursor.lockState = CursorLockMode.None;
    	    	Cursor.visible = true;
		    }
    		else if(pauseGameCanvas.enabled == true && Input.GetKeyDown(KeyCode.Escape))
	    	{
		    	pauseGameCanvas.enabled = false;
            	Time.timeScale = 1;
            	FindObjectOfType<WeaponSwitcher>().enabled = true;
    	    	FindObjectOfType<StarterAssets.FirstPersonController>().enabled = true;
    			//GetComponentInChildren<CrossbowHandler>().enabled = true;
                //GetComponentInChildren<RifleHandler>().enabled = true;
        		Cursor.lockState = CursorLockMode.Locked;        			
		    	Cursor.visible = false;
		    }
    }
}
