using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {

    public LayerMask catLayerMask;
    public float barkRadius = 2f;
    public float barkForce = 60f;
    public float refreshMultiplier = 2f;
    public GameObject barkEffect;

    Animator animator;
    ProgressBar bark;

    private void Start() {
        bark = GameObject.FindWithTag("BarkBar").GetComponent<ProgressBar>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() {
        bark.Current += Time.fixedDeltaTime * refreshMultiplier;
        if (bark.CanUse && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl))) {
            bark.Current = 0;
            PerformBark();
        }
	}

    void PerformBark() {
        animator.SetTrigger("Bark");
        GameObject be = Instantiate(barkEffect);
        be.transform.position = transform.position;

        Collider2D[] nearbyCats = Physics2D.OverlapCircleAll(transform.position, barkRadius, catLayerMask);
        foreach (Collider2D cat in nearbyCats) {
            Rigidbody2D rb = cat.GetComponent<Rigidbody2D>();
            if (rb == null) continue;
            ApplyBark(rb);
        }
    }

    void ApplyBark(Rigidbody2D catRigidBody) {
        var direction = (catRigidBody.transform.position - transform.position);
        float falloff = 1 - (direction.magnitude / barkRadius);
        catRigidBody.AddForce(direction.normalized * (falloff <= 0f ? 0f : barkForce) * falloff);
    }
}
