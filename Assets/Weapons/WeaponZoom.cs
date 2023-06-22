using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private float originalFOV = 70f;
    [SerializeField] private float zoomFOV = 20f;
    [SerializeField] private float originalSensitivity = 0.5f;
    [SerializeField] private float zoomSensitivity = 0.1f;

    private bool zoomedInToggle = false;

    private void OnEnable() {
        playerInput.actions["Zoom"].performed += WeaponZoom_performed;
    }

    private void OnDisable() {
        playerInput.actions["Zoom"].performed -= WeaponZoom_performed;
        SetFOV(originalFOV);
        SetSensitivity(originalSensitivity);
    }

    private void WeaponZoom_performed(InputAction.CallbackContext obj) {
        zoomedInToggle = !zoomedInToggle;
        if (zoomedInToggle) {
            SetFOV(zoomFOV);
            SetSensitivity(zoomSensitivity);
        } else {
            SetFOV(originalFOV);
            SetSensitivity(originalSensitivity);
        }
    }

    private void SetFOV(float fov) {
        virtualCamera.m_Lens.FieldOfView = fov;
    }

    private void SetSensitivity(float sensitivity) {
        firstPersonController.RotationSpeed = sensitivity;
    }
}
