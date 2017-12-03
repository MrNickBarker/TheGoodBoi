using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    List<GameObject> allCats = new List<GameObject>();
    List<GameObject> herderdCats = new List<GameObject>();
    AudioSource sound;
	
	void Start () {
        allCats.AddRange(GameObject.FindGameObjectsWithTag("Cat"));
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Cat") {
            sound.Play();
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
