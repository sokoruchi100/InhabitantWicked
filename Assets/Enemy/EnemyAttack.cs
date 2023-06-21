using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private PlayerHealth playerHealth;
    [SerializeField] private float damage = 40f;

    private void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void AttackHitEvent() {
        if (playerHealth == null) return;

        playerHealth.TakeDamage(damage);
    }
}
