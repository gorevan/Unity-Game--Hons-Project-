using UnityEngine;
using System.Collections;

public class ItemRespawn: MonoBehaviour {
    
    public int gainAmount = 10; //How much XP the player gains from pick up 

    SphereCollider sc; //Reference to Sphere Collider
    MeshRenderer mr; //Reference to Mesh Renderer
    PauseController pc; //Reference to Pause Controller
    Player player; //Reference to Player
    Stat stat; //Reference to Stat

    private FMODUnity.StudioEventEmitter eventEmitterRef; //Reference to FMOD Studio Event Emitter

    public bool triggered;  //Triggered set to a bool

    void Start()
    {
        //Setting up references
        sc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();       
        pc = GetComponent<PauseController>();
        player = gameObject.GetComponent<Player>();
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
    }
   
    void OnTriggerEnter(Collider collider)
    {           
        if(collider.tag == "Player") //If player enters the pick up item then...
        {
            triggered = true; //Triggered is set to true
            sc.enabled = false; //Sphere Collider is turned off
            mr.enabled = false; //Mesh Renderer is turned off
                        
            Destroy(this.gameObject); //Destroy pick up item
            eventEmitterRef.Play(); //Play sound from FMOD

            PauseController.XPMoney += gainAmount; //Apply gainAmount to players current XP         
        }        
    }   
}
