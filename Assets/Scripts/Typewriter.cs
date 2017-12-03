using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(AudioSource))]
public class Typewriter : MonoBehaviour {

    Text label;
    AudioSource sound;
    string text;
    float delay = 2;
    int index = 0;

	void Start () {
        sound = GetComponent<AudioSource>();
        label = GetComponent<Text>();
        text = label.text;
        label.text = "";
	}
	
	void Update () {
		delay -= Time.deltaTime;
        if (delay < 0 && index < text.Length) {
            sound.Play();
            delay = text[index] == ' ' ? 0.5f : Random.Range(0.1f, 0.2f);
            label.text += text[index];
            index++;
        }
        else if (index >= text.Length) {
            this.enabled = false;
        }
	}
}
