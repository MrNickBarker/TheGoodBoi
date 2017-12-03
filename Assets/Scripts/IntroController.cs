using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

    public void FinishIntro() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
