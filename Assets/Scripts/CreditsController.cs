using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

    public void OnRestart() {
        SceneManager.LoadScene(0);
    }

    public void OnWebsite() {
        Application.OpenURL("http://www.pixelome.com/");
    }

    public void OnTwitter() {
        Application.OpenURL("http://www.twitter.com/pixelome/");
    }
}
