using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    private bool isDead = false;

    public bool IsDead() {
        return isDead;
    }

    public void TakeDamage(float amount) {
        hitPoints -= amount;

        if (hitPoints <= 0) {
            Die();
        }
        BroadcastMessage("OnDamageTaken");
    }

    private void Die() {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
