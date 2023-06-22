using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] private int ammoAmount = 1;
    [SerializeField] private AmmoType ammoType;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == PLAYER_TAG) {
            other.gameObject.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
