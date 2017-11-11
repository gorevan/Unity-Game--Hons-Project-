using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetryorQuitGame : MonoBehaviour {

	public void ToggleRetryGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ToggleQuitGame()
    {
        Application.Quit();
    }
}
