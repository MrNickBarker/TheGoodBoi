using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour {

    public Animator animator;
    Rigidbody2D rb;
    bool walking = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        bool shouldWalk = rb.velocity.magnitude > 0.2f;
        if (shouldWalk != walking) {
            walking = shouldWalk;
            animator.SetBool("Walking", walking);
        }
	}
}
