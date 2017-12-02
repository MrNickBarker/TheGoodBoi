using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Cat : MonoBehaviour {

    enum State {
        Still,
        Moving
    }

    public float minimumActionInterval = 1f;
    public float maximumActionInterval = 2f;
    public float force = 0.5f;
    public float detectionRange = 1f;
    public List<string> detectionTags;

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
    }

    void PerformAction() {
        if (state == State.Still) return;

        List<Angle> blockedDirections = BlockedDirections();
        int attempt = 20;
        float randomAngle = 0;
		bool angleIsBlocked = false;

        do {
            angleIsBlocked = false;
            randomAngle = Random.Range(0f, 359f);
            attempt--;

            foreach (Angle blockedAngle in blockedDirections) {
                if (blockedAngle.contains(randomAngle)) {
                    angleIsBlocked = true;
					continue;
                }
            }
        } while (attempt > 0 && angleIsBlocked);

        if (attempt <= 0 && angleIsBlocked) {
            state = State.Still;
            Debug.LogWarning("All angles are blocked");
            return;
        }

        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        rb.AddForce(randomDirection * force, ForceMode2D.Impulse);
    }

    List<Angle> BlockedDirections() {
        List<Angle> blockedDirections = new List<Angle>();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detectionRange, Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            if (detectionTags.Contains(hit.collider.tag)) {
                Vector3 dir = hit.point - (Vector2)transform.position;
                float blockedAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                blockedDirections.Add(new Angle(blockedAngle, 40f));
            }
        }
        return blockedDirections;
    }

    protected struct Angle {
        float start;
        float end;

        public bool contains(float number) {
            return number >= start && number <= end;
        }

        public Angle(float middle, float buffer) {
            this.start = middle - buffer;
            this.start = (this.start + 360) % 360;
            this.end = middle + buffer;
            this.end = (this.end + 360) % 360;
        }
    }
}
