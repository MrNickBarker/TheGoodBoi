using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    List<GameObject> allCats = new List<GameObject>();
    List<GameObject> herderdCats = new List<GameObject>();
	
	void Start () {
        allCats.AddRange(GameObject.FindGameObjectsWithTag("Cat")); 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Cat") {
            herderdCats.Add(collision.gameObject);
            if (herderdCats.Count == allCats.Count) {
                FindObjectOfType<LevelController>().OnLevelComplete();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Cat") {
            herderdCats.Remove(collision.gameObject);
        } 
    }
}
