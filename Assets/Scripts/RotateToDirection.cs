using UnityEngine;

public class RotateToDirection : MonoBehaviour {

    public GameObject model;
    Vector3 lastPosition = Vector2.zero;
    	
	void FixedUpdate () {
        if (lastPosition == transform.position) return;
        Vector3 dir = lastPosition - transform.position;
        float towards = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector3 angles = model.transform.eulerAngles;
        angles.z = towards;
        model.transform.eulerAngles = angles;
		lastPosition = transform.position;
	}
}
