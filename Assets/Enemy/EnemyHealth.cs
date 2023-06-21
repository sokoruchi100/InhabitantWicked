using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;

    public void TakeDamage(float amount) {
        BroadcastMessage("OnDamageTaken");

        hitPoints -= amount;

        if (hitPoints <= 0) {
            Destroy(gameObject);
        }
    }
}
