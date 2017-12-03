using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {

    public int timeLimit = 30;
	Text label;
    Coroutine tick;

    private void Start() {
        label = GameObject.FindWithTag("TimeLabel").GetComponent<Text>();
        Countdown(timeLimit);
    }

    private void OnDisable() {
        if (tick != null) StopCoroutine(tick);
    }

    private void OnDestroy() {
        if (tick != null) StopCoroutine(tick);
    }

    public void Countdown(int seconds) {
        if (tick != null) StopCoroutine(tick);
        tick = StartCoroutine(Tick(seconds));
        UpdateText(seconds);
    }
	
    IEnumerator Tick(int seconds) {
        while (seconds > 0) {
            seconds--;
			yield return new WaitForSeconds(1);
            UpdateText(seconds);
        }
        GetComponent<LevelController>().OnTimeRanOut();
    }

    void UpdateText(int seconds) {
        if (label != null) {
			label.text = seconds.ToString();
        }
    }
}
