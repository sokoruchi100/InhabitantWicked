using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 25f;

    private void OnEnable() {
        playerInput.actions["Shoot"].performed += Weapon_performed;
    }

    private void Weapon_performed(InputAction.CallbackContext obj) {
        Shoot();
    }

    private void Shoot() {

        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {
            Debug.Log("I hit a: " + hit.transform.name);
            if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
                enemyHealth.TakeDamage(damage);
            }
        } else {
            return;
        }
        
    }
}
