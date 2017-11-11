using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SaveGame : MonoBehaviour
{

    public Transform Player;

    void Awake()
    {
        Player.position = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
        Player.eulerAngles = new Vector3(0, PlayerPrefs.GetFloat("ScreenY"));
    }
    public void SaveGameSettings(bool Quit)
    {
        PlayerPrefs.SetFloat("x", Player.position.x);
        PlayerPrefs.SetFloat("y", Player.position.y);
        PlayerPrefs.SetFloat("z", Player.position.z);
        PlayerPrefs.SetFloat("ScreenY", Player.eulerAngles.y);
        
        if (Quit)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main Menu");
        }
    }
}