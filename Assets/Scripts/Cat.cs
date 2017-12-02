using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cat : MonoBehaviour {

    enum State {
        Still,
        Moving
    }

    public float minimumActionInterval = 1f;
    public float maximumActionInterval = 2f;
    public float force = 0.5f;

    float interval = 0;
    float lastAction = 0;
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
        state = Random.Range(0, 2) == 0 ? State.Still : State.Moving;
        Debug.Log(state);
    }

    void PerformAction() {
        if (state == State.Still) return;

        float randomAngle = Random.Range(0f, 360f);
        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        rb.AddForce(randomDirection * force, ForceMode2D.Impulse);
    }
}
