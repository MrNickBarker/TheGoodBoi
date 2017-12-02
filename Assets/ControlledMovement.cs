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
        float vertical = Input.GetAxis("Vertical") * speed;
        float horizontal = Input.GetAxis("Horizontal") * speed;
        rb.MovePosition((Vector2)transform.position + new Vector2(horizontal, vertical) * Time.deltaTime);
	}
}
