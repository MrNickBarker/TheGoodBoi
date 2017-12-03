using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public enum State {
        Play,
        FinishedLevel,
        GameOver
    }

    public GameObject gameOver;
    public GameObject nextLevel;

    public void SetState(State state) {
        gameOver.SetActive(state == State.GameOver);
        nextLevel.SetActive(state == State.FinishedLevel);
    }

    public void RestartLevel() {
        FindObjectOfType<LevelController>().OnRestartLevel();
    }

    public void NextLevel() {
        FindObjectOfType<LevelController>().OnNextLevel();
    }
}
