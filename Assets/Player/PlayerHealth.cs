using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    private DeathHandler deathHandler;

    private void Start() {
        deathHandler = GetComponent<DeathHandler>();
    }

    public void TakeDamage(float amount) {
        hitPoints -= amount;
        if (hitPoints <= 0) {
            deathHandler.OnDeath();
        }
    }
}
