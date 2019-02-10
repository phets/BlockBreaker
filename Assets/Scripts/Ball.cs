using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public AudioClip bounceAudioClip;
    public bool AddRandomness = true;
    private Paddle paddle;
    private bool hasStarted = false;
    private Vector3 paddle2Ball;

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddle2Ball = this.transform.position - paddle.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted) {
            //Lock the ball relative to the paddle
            this.transform.position = paddle.transform.position + paddle2Ball;
            //Wait for mouse press to launch
            if (Input.GetMouseButtonDown(0)) {

                float mouseCursorSpeed = Mathf.Clamp(Input.GetAxis("Mouse X") / (10*Time.deltaTime),-10.0f,10.0f);
                Vector2 mouseVelocity = new Vector2(mouseCursorSpeed, 5.0f);
                this.GetComponent<Rigidbody2D>().velocity = mouseVelocity;
                hasStarted = true;
            }
        } else if(this.transform.position.y < -1.0f) {
            this.transform.position = new Vector3(0.0f, -1.0f, 5.0f);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (hasStarted) {
            AudioSource.PlayClipAtPoint(bounceAudioClip, transform.position);

            if (AddRandomness) {
                Vector2 perturbation = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
                this.GetComponent<Rigidbody2D>().velocity += perturbation;
                float ballSpeed = this.GetComponent<Rigidbody2D>().velocity.magnitude;
                Debug.Log("Ball speed = " + ballSpeed);
            }
        }
    }
}
