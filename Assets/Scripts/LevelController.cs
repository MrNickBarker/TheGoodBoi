using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public GameObject gameOverPanel;

	bool isGameOver = false;
    public bool IsGameOver {
        get {
			return isGameOver;
        }
    }

    public void OnTimeRanOut() {
        gameOverPanel.SetActive(true);
        isGameOver = true;

        GameObject dog = GameObject.FindWithTag("Dog");
        dog.GetComponent<ControlledMovement>().enabled = false;
        dog.GetComponent<Bark>().enabled = false;
    }

    public void OnRestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
