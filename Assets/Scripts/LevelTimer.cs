using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {

	public Text label;
    public int timeLimit = 30;
    Coroutine tick;

    private void Start() {
        Countdown(timeLimit);
    }

    private void OnDisable() {
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
        label.text = seconds.ToString();
    }
}
