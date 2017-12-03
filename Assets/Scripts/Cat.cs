using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Cat : MonoBehaviour {

    enum State {
        Still,
        Moving,
        OnGoal
    }

    public float minimumActionInterval = 1f;
    public float maximumActionInterval = 2f;
    public float force = 0.5f;
    public float detectionRange = 1f;
    public List<string> detectionTags;

    float interval = 0;
    float lastAction = 0;
    float goalExitAngle = 0;
	Rigidbody2D rb;
    State state = State.Still;

	void Start() {
        RandomizeInterval();
        rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
        lastAction += Time.deltaTime;
        if (lastAction > interval) {
            lastAction = 0;
            RandomizeInterval();
            RandomizeState();
            PerformAction();
        }
    }

    void RandomizeInterval() {
        interval = Random.Range(minimumActionInterval, maximumActionInterval);
    }

    void RandomizeState() {
        if (state == State.OnGoal) {
            if (Random.Range(0, 4) == 0) {
                state = State.Still;
                rb.isKinematic = false;
                MoveAtAngle(goalExitAngle, 1f);
            }   
        }
        else {
			state = Random.Range(0, 2) == 0 ? State.Still : State.Moving;
        }
    }

    void PerformAction() {
        if (state == State.Still) return;

        float? angle = Angle.UnblockedRandom(transform.position, detectionRange, detectionTags);
        if (angle == null) {
            state = State.Still;
            Debug.LogWarning("All angles are blocked");
        }
        else {
            MoveAtAngle(angle.Value, force);
        }
    }

    void MoveAtAngle(float angle, float movementForce) {
        Vector2 randomDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb.AddForce(randomDirection * movementForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Goal") {
			state = State.OnGoal;
			rb.isKinematic = true;
            rb.position = collision.transform.position;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            gameObject.layer = 10; // ignored cat layer
            goalExitAngle = collision.transform.rotation.eulerAngles.z + Random.Range(150, 210);
            GetComponentInChildren<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Goal") {
            gameObject.layer = 8; // cat layer
            GetComponentInChildren<Renderer>().enabled = true;
        }
    }
}
