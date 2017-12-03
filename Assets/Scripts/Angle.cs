using System.Collections.Generic;
using UnityEngine;

struct Angle {
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

    static public List<Angle> Blocked(Vector2 position, float range, List<string> detectionTags) {
        List<Angle> blockedDirections = new List<Angle>();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, range, Vector2.zero);
        foreach (RaycastHit2D hit in hits) {
            if (detectionTags.Contains(hit.collider.tag)) {
                Vector3 dir = hit.point - position;
                float blockedAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                blockedDirections.Add(new Angle(blockedAngle, 40f));
            }
        }
        return blockedDirections;
    }

    static public float? UnblockedRandom(List<Angle> blockedAngles) {
        int attempt = 20;
        float randomAngle = 0;
        bool angleIsBlocked = false;

        do {
            angleIsBlocked = false;
            randomAngle = Random.Range(0f, 359f);
            attempt--;

            foreach (Angle blockedAngle in blockedAngles) {
                if (blockedAngle.contains(randomAngle)) {
                    angleIsBlocked = true;
                    continue;
                }
            }
        } while (attempt > 0 && angleIsBlocked);

        if (attempt <= 0 && angleIsBlocked) {
            return null;
        }
        return randomAngle;
    }

    static public float? UnblockedRandom(Vector2 position, float range, List<string> detectionTags) {
        return UnblockedRandom(Blocked(position, range, detectionTags));
    }
}
