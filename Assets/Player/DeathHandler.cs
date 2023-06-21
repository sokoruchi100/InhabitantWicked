using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;

    private void Start() {
        gameOverCanvas.enabled = false;
    }

    public void OnDeath() {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverCanvas.enabled = true;
    }
}
