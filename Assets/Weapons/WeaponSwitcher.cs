using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] int currentWeapon = 0;

    private void OnEnable() {
        playerInput.actions["Pistol"].performed += WeaponSwitcher_performedPistol;
        playerInput.actions["Shotgun"].performed += WeaponSwitcher_performedShotgun;
        playerInput.actions["Sniper"].performed += WeaponSwitcher_performedSniper;
    }

    private void OnDisable() {
        playerInput.actions["Pistol"].performed -= WeaponSwitcher_performedPistol;
        playerInput.actions["Shotgun"].performed -= WeaponSwitcher_performedShotgun;
        playerInput.actions["Sniper"].performed -= WeaponSwitcher_performedSniper;
    }

    private void Start() {
        SetWeaponActive();
    }

    private void WeaponSwitcher_performedSniper(InputAction.CallbackContext obj) {
        ProcessKeyInput(2);
    }

    private void WeaponSwitcher_performedShotgun(InputAction.CallbackContext obj) {
        ProcessKeyInput(1);
    }

    private void WeaponSwitcher_performedPistol(InputAction.CallbackContext obj) {
        ProcessKeyInput(0);
    }

    private void ProcessKeyInput(int weaponIndex) {
        currentWeapon = weaponIndex;
        SetWeaponActive();
    }

    private void SetWeaponActive() {
        int weaponIndex = 0;

        foreach (Transform weapon in transform) {
            if (weaponIndex == currentWeapon) {
                weapon.gameObject.SetActive(true);
            } else {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
