using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public Camera topDownCamera;

    private int currentCameraIndex;

    void Start()
    {
        // Setze alle Kameras initial inaktiv
        firstPersonCamera.gameObject.SetActive(false);
        thirdPersonCamera.gameObject.SetActive(false);
        topDownCamera.gameObject.SetActive(false);

        // Aktivere die erste Kamera (z.B. Ego-Perspektive)
        currentCameraIndex = 0;
        SwitchCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Wechsle zur nächsten Kamera
            currentCameraIndex = (currentCameraIndex + 1) % 3;
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // Deaktiviere alle Kameras
        firstPersonCamera.gameObject.SetActive(false);
        thirdPersonCamera.gameObject.SetActive(false);
        topDownCamera.gameObject.SetActive(false);

        // Aktiviere die aktuelle Kamera basierend auf dem Index
        switch (currentCameraIndex)
        {
            case 0:
                firstPersonCamera.gameObject.SetActive(true);
                break;
            case 1:
                thirdPersonCamera.gameObject.SetActive(true);
                break;
            case 2:
                topDownCamera.gameObject.SetActive(true);
                break;
        }
    }
}
