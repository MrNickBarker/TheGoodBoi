using UnityEngine;

public class Tutorial : MonoBehaviour {
	
	void Update () {
        if (Mathf.Approximately(Input.GetAxis("Horizontal"), 0) == false ||
            Mathf.Approximately(Input.GetAxis("Vertical"), 0) == false ||
            Input.GetKeyDown(KeyCode.Space)) {
            Destroy(gameObject);
        }
	}
}
