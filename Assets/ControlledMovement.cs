using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlledMovement : MonoBehaviour {

    public float speed = 1;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        bool sprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = speed * (sprinting ? 2f : 1f);
        float vertical = Input.GetAxis("Vertical") * currentSpeed;
        float horizontal = Input.GetAxis("Horizontal") * currentSpeed;
        rb.MovePosition((Vector2)transform.position + new Vector2(horizontal, vertical) * Time.deltaTime);
	}
}
