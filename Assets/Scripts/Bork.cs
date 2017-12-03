using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bork : MonoBehaviour {

    public void OnAnimationFinished() {
        Destroy(gameObject);
    }
}
