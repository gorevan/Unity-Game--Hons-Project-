using UnityEngine;
using System.Collections;
using UnityStandardAssets;
using UnityStandardAssets.Characters.FirstPerson;
using System;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;


public class PauseController : MonoBehaviour
{    
    public Transform canvasPause; //Reference to canvasPause
    public Transform playerFPS; //Reference to the playerFPS
    public Transform pauseMenu; //Reference to the pauseMenu
    public Transform soundMenu; //Reference to the soundMenu
    public Transform videoSettingsMenu; //Reference to the videoSettingsMenu
    public Transform controlsMenu; //Reference to the controlsMenu

    public Slider[] volumeSliders; //Reference to array of sliders

    public static PauseController pc; //Reference to PauseController

    SaveGame saveGame; //Reference to the saveGame  
    MouseLook ml;
               
    [SerializeField]
    private GameObject XPMenu; //Reference to the XPMenu

    [SerializeField]
    private int startingXP; //Reference to the startingXP
    public static int XPMoney; //Reference to XPMoney

    public delegate void XPMenuCallback(bool active);
    public XPMenuCallback onToggleXPMenu;
   
    void Awake()
    {
        if (pc == null) //If Player Controller is not on then...
        {
            pc = GameObject.FindGameObjectWithTag("PC").GetComponent<PauseController>(); //Find Game Object which has the tag if "PC" and obtain Pause Controller
        }
        ml = GetComponent<MouseLook>();                                            
    }

    void Start()
    {
        Time.timeScale = 1; //Time is set to on when game starts
        XPMoney = startingXP; //Setting current XPMoney to the startingXP      
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape)) //If player presses 'Esc' then...
        {
            Pause(); //Toggle Pause Menu
        }

        if (Input.GetKeyDown(KeyCode.U)) //If player presses 'U' then... 
        {
            ToggleXPMenu(); //XP Menu will be displayed
        }
        
    }
      
    private void ToggleXPMenu()
    {

        XPMenu.SetActive(!XPMenu.activeSelf); //Toggle XP Menu
        onToggleXPMenu.Invoke(XPMenu.activeSelf); //Active XP Menu
        Cursor.visible = true; //Cursor will be visible when XP Menu is toggled   
        Time.timeScale = 0; //Time is not on
        canvasPause.gameObject.SetActive(false); //Disable Pause Menu Canvas
        Camera.main.GetComponent<Blur>().enabled = true;
        Camera.main.GetComponent<MouseLook>().enabled = false;
        AudioListener.volume = 0; //Turn Sound Off
              
        if (XPMenu.gameObject.activeInHierarchy == false) //If the XP Menu isn't being toggled then...
        {
            Cursor.visible = false; //Cursor will not be visible
            Time.timeScale = 1; //Time is set to on
            Camera.main.GetComponent<Blur>().enabled = false;
            Camera.main.GetComponent<MouseLook>().enabled = true;
            AudioListener.volume = 1; //Turn Sound On
        }

    }

    public void Pause()
    {
        if (canvasPause.gameObject.activeInHierarchy == false) //If the Pause Canvas isn't active then...
        {
            if (pauseMenu.gameObject.activeInHierarchy == false) //If the Pause Menu isn't active then...
            {
                pauseMenu.gameObject.SetActive(true); //Pause Menu is activated
                soundMenu.gameObject.SetActive(false); //Sound Menu is deactivated
                videoSettingsMenu.gameObject.SetActive(false); //Video Settings Menu is deactivated
                controlsMenu.gameObject.SetActive(false); //Controls Menu is deactivated
                XPMenu.gameObject.SetActive(false);
                Camera.main.GetComponent<MouseLook>().enabled = false;
                AudioListener.volume = 0; //Turn Sound Off
                


            }
            canvasPause.gameObject.SetActive(true); //Canvas Pause is toggled on
            Time.timeScale = 0; //Time will not be on
            saveGame = gameObject.GetComponent<SaveGame>();
            saveGame.SaveGameSettings(false);
            Cursor.visible = true; //Cursor will be visible
            Camera.main.GetComponent<Blur>().enabled = true;
            onToggleXPMenu.Invoke(pauseMenu);

        }
        else
        {
            canvasPause.gameObject.SetActive(false); //Canvas Pause will not be deactivated
            Time.timeScale = 1; //Time is set to on
            Cursor.visible = false; //Cursor is not visible
            Camera.main.GetComponent<Blur>().enabled = false;
            Camera.main.GetComponent<MouseLook>().enabled = true;
            onToggleXPMenu.Invoke(!pauseMenu);
            AudioListener.volume = 1; //Turn Sound On
        }
    }

    public void Sounds(bool Open)
    {
        if (Open)
        {
            soundMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);

        }
        if (!Open)
        {
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
    public void VideoSettings(bool Open)
    {
        if (Open)
        {
            videoSettingsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);

        }
        if (!Open)
        {
            videoSettingsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
    public void Controls(bool Open)
    {
        if (Open)
        {
            controlsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);

        }
        if (!Open)
        {
            controlsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    } 
  
}