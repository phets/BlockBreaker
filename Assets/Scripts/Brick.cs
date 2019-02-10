using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public Sprite[] damagedSprites;
    private int HP,maxHP;
    private LevelManager levelManager;
    public static int breakableBrickCount = 0;
    public AudioClip damageAudioClip, destroyAudioClip;

    private void Start() {
        maxHP = damagedSprites.Length + 1;
        HP = maxHP;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        breakableBrickCount = GameObject.FindGameObjectsWithTag("Breakable").Length;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (this.tag == "Breakable") HandleHits();
    }

    private void HandleHits() {
        HP--;
        if (HP <= 0) {
            AudioSource.PlayClipAtPoint(destroyAudioClip, transform.position);
            Destroy(gameObject);
            breakableBrickCount--;
            levelManager.BrickDestroyed();
            Debug.Log("breakableBrickCount = " + breakableBrickCount);
        } else {
            gameObject.GetComponent<SpriteRenderer>().sprite = damagedSprites[HP - 1];
            AudioSource.PlayClipAtPoint(damageAudioClip, transform.position);
        }

        if (breakableBrickCount <= 0) {
            levelManager.LoadNextLevel();
        }
    }
}
