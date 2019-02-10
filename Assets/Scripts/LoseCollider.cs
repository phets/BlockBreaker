using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

    private LevelManager levelManager;

    private void Start() {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("LoseCollider.OnTriggerEnter2D");
        levelManager.LoadLevel("Lose_Screen");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("LoseCollider.OnCollisionEnter2D");
    }
}
