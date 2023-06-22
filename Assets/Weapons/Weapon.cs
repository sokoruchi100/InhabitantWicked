using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private const string SHOOT_ACTION = "Shoot";

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera FPCamera;
    [SerializeField] private ParticleSystem muzzleFlashVFX;
    [SerializeField] private Light muzzleFlashLight;
    [SerializeField] private GameObject hitVFXGameObject;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private float rof = 1f;
    [SerializeField] private float flashOn = 0.2f;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;

    private bool canShoot = true;
    

    private void OnEnable() {
        playerInput.actions[SHOOT_ACTION].performed += Weapon_performed;
        canShoot = true;
    }

    private void OnDisable() {
        playerInput.actions[SHOOT_ACTION].performed -= Weapon_performed;
    }

    private void Weapon_performed(InputAction.CallbackContext obj) {
        if (canShoot) {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot() {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0) {
            StartCoroutine(PlayMuzzleFlash());
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(rof);
        canShoot = true;
    }

    private IEnumerator PlayMuzzleFlash() {
        muzzleFlashVFX.Play();
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(flashOn);
        muzzleFlashLight.enabled = false;
    }

    private void ProcessRaycast() {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {

            CreateHitImpact(hit);

            if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
                enemyHealth.TakeDamage(damage);
            }
        } else {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit) {
        GameObject newHitVFXGameObject = Instantiate(hitVFXGameObject, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(newHitVFXGameObject, 1);
    }
}
