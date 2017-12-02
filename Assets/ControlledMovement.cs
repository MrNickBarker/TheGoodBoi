using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlledMovement : MonoBehaviour {

    public float walkingSpeed = 1;
    public float maxWalkingSpeed = 4f;
    public float runningSpeed = 2f;
    public float maxRunningSpeed = 8f;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        bool running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = running ? runningSpeed : walkingSpeed;
		float horizontal = Input.GetAxis("Horizontal") * currentSpeed;
        float vertical = Input.GetAxis("Vertical") * currentSpeed;
        rb.AddForce(new Vector2(horizontal, vertical), ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, running ? maxRunningSpeed : maxWalkingSpeed);
	}
}
