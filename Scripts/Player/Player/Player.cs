using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour
{
    public float flashSpeed = 0.5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set which will flash
    public Image damageImage;

    private Stats stats;

    [SerializeField]
    public Stat health;

    [SerializeField]
    public Stat stamina;

    [SerializeField]
    private Stat shield;

    [SerializeField]
    private int upgradeCost = 120;

    ItemRespawn ir;
    Connection connection;

    PauseController pc;

    public int fallBoundary = -20;
    
    bool damaged;
    
    public Transform gameOver; //Reference to the gameOver screen   
    public Transform warningScreen;
    
    public MusicControl musicControl;

    CharacterController charactercontroller;
    

    //  public float drownTime = 600f;

    private FMODUnity.StudioEventEmitter eventEmitterRef;

    float time = 5f;

    void Awake()
    {
        health.Initialize();
        stamina.Initialize();
        shield.Initialize();       
    }


    void Start()
    {       
        PauseController.pc.onToggleXPMenu += OnXPMenuToggle;
        stats = Stats.instance;
        ir = GetComponent<ItemRespawn>();
        connection = GetComponent<Connection>();
        pc = GetComponent<PauseController>();
        charactercontroller = GetComponent<CharacterController>();
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
        


        AudioListener.volume = 1; //turn sound on

    }

    void Update()
    {
        
        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer(9999999);     //Kill Player if he falls through map.
        }
       
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        
        if (health.CurrentVal == 0)
        {            
            musicControl.DeathMusic();
            ToggleGameOver();
            eventEmitterRef.Play();
            time -= Time.deltaTime;                       
        }

        
        damaged = false;
    }

    void OnXPMenuToggle(bool active)
    {
        GetComponent<MouseLook>().enabled = !active;
        GetComponent<FPSInputController>().enabled = !active;
        GetComponent<CharacterMotor>().enabled = !active;
        GetComponent<CharacterController>().enabled = !active;
    }

    public void DamagePlayer(float damage)
    {
        health.currentVal -= damage;
        if (health.currentVal <= 0)
        {
            ToggleGameOver();                                  
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            return;
        if (collider.tag == "Health Collider" && health.CurrentVal < health.MaxVal)
        {
            health.CurrentVal += 10;
        }

        if (collider.tag == "Health Collider" && health.CurrentVal == health.MaxVal)
        {
            shield.CurrentVal += 10;
        }

        if (collider.tag == "Health Max Collider" && health.CurrentVal < health.MaxVal)
        {
            health.CurrentVal = health.MaxVal;
        }

        if (collider.tag == "Health Max Collider" && health.CurrentVal == health.MaxVal)
        {
            shield.CurrentVal = shield.MaxVal;
        }
       
        if (collider.tag == "Enemy")
        {
            damaged = true;
            shield.CurrentVal -= 10;
            damageImage.color = flashColour;
            AudioManager.instance.PlayerSound2D("Shout");
            if (shield.CurrentVal <= 0)
            {                
                health.CurrentVal -= 10;               
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
                damageImage.color = Color.clear;
            }
            if (shield.CurrentVal == 0)
            {
                AudioManager.instance.PlayerSound2D("Shield");
            }
        }

        if (collider.tag == "Death Barrier")
        {
            ToggleGameOver();
            warningScreen.gameObject.SetActive(false);
            AudioManager.instance.PlaySound("Death", transform.position);
           
        }

        if (collider.tag == "Warning")
        {
            ToggleWarning();
        }
    }
        public void OnTriggerExit(Collider collider)
    {
        warningScreen.gameObject.SetActive(false);
    }
    

    public void UpgradeHealth()
    {
        if (PauseController.XPMoney < upgradeCost)
        {

            return;
        }
        else
        {

            health.MaxVal += 10;
            health.CurrentVal = health.MaxVal;
            PauseController.XPMoney -= upgradeCost;
        }

    }

    public void UpgradeShield()
    {
        if (PauseController.XPMoney < upgradeCost)
        {
            return;
        }
        shield.MaxVal += 10;
        shield.CurrentVal += 10;
        PauseController.XPMoney -= upgradeCost;
    }
    public void ToggleGameOver()
    {        
            if (gameOver.gameObject.activeInHierarchy == false) //If the Pause Menu isn't active then...
            {
                gameOver.gameObject.SetActive(true); //Pause Menu is activated

                Camera.main.GetComponent<MouseLook>().enabled = false;
                OnXPMenuToggle(true);

            }
            gameOver.gameObject.SetActive(true); //Canvas Pause is toggled on
            Time.timeScale = 0; //Time will not be on
            OnXPMenuToggle(true);

            Cursor.visible = true; //Cursor will be visible
            Camera.main.GetComponent<Blur>().enabled = true;
        
        }
          public void ToggleWarning()
    {
        if(warningScreen.gameObject.activeInHierarchy == false)
        {
            warningScreen.gameObject.SetActive(true);           
        }
    }
    void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}


