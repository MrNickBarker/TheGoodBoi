using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public void OnTimeRanOut() {
        FindObjectOfType<CanvasController>().SetState(CanvasController.State.GameOver);
        DisableDog();
        DisableCats();
    }

    public void OnRestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnLevelComplete() {
        FindObjectOfType<CanvasController>().SetState(CanvasController.State.FinishedLevel);
        DisableDog();
        DisableCats();
    }

    public void OnNextLevel() {      
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void DisableDog() {
        GameObject dog = GameObject.FindWithTag("Dog");
        dog.GetComponent<ControlledMovement>().enabled = false;
        dog.GetComponent<Bark>().enabled = false;
    }

    void DisableCats() {
        Cat[] cats = FindObjectsOfType<Cat>();
        foreach (Cat cat in cats) {
            cat.enabled = false;
        }
    }
}
