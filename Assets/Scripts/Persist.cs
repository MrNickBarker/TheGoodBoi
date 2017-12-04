using UnityEngine;

public class Persist : MonoBehaviour {

    public void Awake() {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }
}
