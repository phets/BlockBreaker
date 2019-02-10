using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public static bool TestingMode = true;
    public static Vector2 paddleVelocity;

    // Reference to the ball so we can keep the paddle under it for TestingMode
    private Ball ball;
	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        paddleVelocity = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (!TestingMode) {
            MoveWithMouse();
        } else {
            MoveWithAI();
        }
    }

    private void MoveWithMouse() {
        float mouseX = Mathf.Clamp((Input.mousePosition.x / Screen.width) * 16f, 0.5f, 15.5f);
        float mouseCursorSpeed = Mathf.Clamp(Input.GetAxis("Mouse X") / (10 * Time.deltaTime), -10.0f, 10.0f);
        paddleVelocity.Set(mouseCursorSpeed, 0.0f);
        //Debug.Log("paddleVelocity = " + paddleVelocity);

        Vector3 paddlePos = new Vector3(mouseX, 0.5f, 0.0f);
        this.transform.position = paddlePos;
    }

    private void MoveWithAI() {
        Vector3 paddlePos = new Vector3(ball.transform.position.x, 0.5f, 0.0f);
        this.transform.position = paddlePos;
    }
}
