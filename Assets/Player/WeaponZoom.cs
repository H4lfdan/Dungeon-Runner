using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    Camera fpsCamera;
    FirstPersonController fpsController;
    private float zoomInFOV = 20f;
    private float zoomInSensitivity = .6f;
    private float zoomOutFOV = 60f;
    private float zoomOutSensitivity = 1f;
    private bool isZoomed = false;
    void Start()
    {
        fpsCamera = GetComponentInParent<Camera>();
        fpsController = GetComponentInParent<FirstPersonController>();
    }

    private void OnDisable()
    {
        ZoomOut();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(!isZoomed)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomIn()
    {
        isZoomed = true;
        fpsCamera.fieldOfView = zoomInFOV;
        fpsController.RotationSpeed = zoomInSensitivity;
    }

    private void ZoomOut()
    {
        isZoomed = false;
        fpsCamera.fieldOfView = zoomOutFOV;
        fpsController.RotationSpeed = zoomOutSensitivity;
    }
}
