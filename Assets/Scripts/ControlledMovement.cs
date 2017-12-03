using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlledMovement : MonoBehaviour {

    public float walkingSpeed = 1;
    public float maxWalkingSpeed = 4f;
    public float runningSpeed = 2f;
    public float maxRunningSpeed = 8f;

    ProgressBar stamina;
    Rigidbody2D rb;
    bool needToStopRunning = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        stamina = GameObject.FindWithTag("StaminaBar").GetComponent<ProgressBar>();
    }

    void FixedUpdate () {
        bool running = needToStopRunning == false && stamina.CanUse && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        if (stamina.CanUse == false) {
			needToStopRunning = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            needToStopRunning = false;
        }

        float currentSpeed = running ? runningSpeed : walkingSpeed;
        float horizontal = Input.GetAxis("Horizontal") * currentSpeed;
        float vertical = Input.GetAxis("Vertical") * currentSpeed;

        stamina.Current += running ? -Time.deltaTime : Time.deltaTime;    
        rb.AddForce(new Vector2(horizontal, vertical), ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, running ? maxRunningSpeed : maxWalkingSpeed);
    }
}
