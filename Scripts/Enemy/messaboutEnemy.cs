using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using FMODUnity;


public class messaboutEnemy : MonoBehaviour
{
    public float sinkSpeed = 0.2f; //The speed at which the enemy sinks through the floor when dead
    public int damageAmount = 10; //How much damage is taken off enemy when player hits
    public int currentHP = 100; //Enemies Health set to 100

    public Transform playerTarget; //Getting players position for enemy to follow
      
    [SerializeField]
    private int upgradeCost = 100; //Upgrade price for players melee strength 

    public MusicControl musicSystem; //Reference to Music Control 

    NavMeshAgent pathfinder; //Reference to Nav Mesh Agent for enemy movement    
    Animator anim; //Reference to animator (for calling animations of enemy)    
    AudioSource audioPlay; //Reference to AudioSource
    SphereCollider enemyCollider; //Reference to SphereCollider
    CapsuleCollider enemyCapsule; //Reference to CapsuleCollider
    PauseController pc; //Reference to PauseController

    bool isSinking; //Whether the enemy has started sinking through the floor.

    private FMODUnity.StudioEventEmitter eventEmitterRef; //Reference to FMOD Studio Event Emitter 
   
    void Start()
    {

        //Setting up references
        pathfinder = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();        
        audioPlay = GetComponent<AudioSource>();
        enemyCollider = GetComponent<SphereCollider>();
        enemyCapsule = GetComponent<CapsuleCollider>();
        pc = GetComponent<PauseController>();
        eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
                
        damageAmount = 10; //How much damage the player takes off of the enemy is set            
    }

    void Update()
    {       
        if (currentHP <= 0) return; //If the enemies health is less than or equal to 0 then it will not return the function below

        if (Vector3.Distance(playerTarget.position, this.transform.position) < 50) //If the distance between player and enemy is less than 50 then...
        {
            pathfinder.enabled = true; //Pathfinding will be enabled
            pathfinder.SetDestination(playerTarget.position); //Set enemies location to the location of the player

            anim.SetBool("isWalking", true); //Walking animation enabled
            anim.SetBool("isAttacking", false); //Attacking animation disabled
            anim.SetBool("isIdle", false); //Idle animation disabled

            musicSystem.BattleMusic(); //Play Battle Music
                        
            if (isSinking) //If the enemy is sinking...
            {                
                this.transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime); //move the enemy down under the terrain by the sinkSpeed per second.
            }

        }
        Vector3 direction = playerTarget.position - this.transform.position; //Defining  direction (subtracting the players position from that of the enemies position)
        if (direction.magnitude > 50) //If distance between player and enemy is greater than 50 then...
        {            
            pathfinder.SetDestination(this.transform.position); //Enemy stops walking towards player

            anim.SetBool("isIdle", true); //Idle animation enabled
            anim.SetBool("isWalking", false); //Walking animation disabled
            anim.SetBool("isAttacking", false); //Attacking animation disabled
            anim.SetBool("isGettingHit", false); //Getting hit animation deactivated    

            musicSystem.CalmMusic(); //Play exploring music                   
        }

        if (Vector3.Distance(playerTarget.position, this.transform.position) < 3) //If distance between player and enemy is greater than 3 then...
        {            
            pathfinder.SetDestination(this.transform.position); //Enemy stops walking towards player

            anim.SetBool("isAttacking", true); //Attacking animation enabled
            anim.SetBool("isWalking", false); //Walking animation disabled
            anim.SetBool("isIdle", false); //Idle animation disabled                  
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy Hit Collider") //If Enemy Collider enters enemy then...
            return; // Do not do any of the below
        if (collider.tag == "Player Attack Collider") //If players collider enters that of the enemies then...
        {
            anim.SetBool("isGettingHit", true); //Getting hit animation activated
            anim.SetBool("isWalking", false); //Walking animation will not play
            anim.SetBool("isAttacking", false); //Attacking animation will not play
            anim.SetBool("isIdle", false); //Idle animation will not play
            currentHP -= damageAmount;  //Take damageAmount off of enemy when collider hits enemy 
            eventEmitterRef.Play(); //Play sound from FMOD Studio Event Emmiter
            AudioManager.instance.PlaySound("Enemy Hurt", transform.position); //Play enemy sound at enemies position     

            if (currentHP <= 0) //If enemies health is less than or equal to 0 then...
            {
                enemyCollider.enabled = false; //Disable SphereCollider to stop glitch where death sound would play several times
                enemyCapsule.enabled = false; //Disable CapsuleCollider to stop glitch where death sound would play several times

                Object.Destroy(gameObject, 6);  //Enemys body will be destroyed after 5 seconds, for efficency.
                pathfinder.SetDestination(this.transform.position); //Enemy stops walking towards player                
                anim.SetBool("isDead", true); //Play death animation
                anim.SetBool("isWalking", false); //Walking animation will not play
                anim.SetBool("isAttacking", false); //Attacking animation will not play
                anim.SetBool("isIdle", false); //Idle animation will not play
                anim.SetBool("isGettingHit", false); //Getting hit animation deactivated

                audioPlay.volume = Random.Range(0.5f, 0.7f); //Random volume between values
                audioPlay.pitch = Random.Range(0.8f, 1.2f); //Random pitch between values 
                musicSystem.CalmMusic(); //Play exploring music
                                
                PauseController.XPMoney += Random.Range(7, 15); //Add random XP from 7 to 15 to XPMoney                 
            }
        }
        else
        {
            anim.SetBool("isGettingHit", false); //If the enemy isn't getting hit, getting hit animation will be disabled                                  
        }        
    }
    
    public void StartSinking()
    {        
        pathfinder.enabled = false; //Enemy will stop moving
               
        GetComponent<Rigidbody>().isKinematic = true; //Find rigidbody of enemy and make it kinematic (use Translate to sink enemy)
        
        isSinking = true; //The enemy will sink
    }

    public void UpgradeMeleeAttack()
    {
        if (PauseController.XPMoney < upgradeCost) //If the current XP of player isn't enough for the upgrade cost then...
        {           
            return; //Do not upgrade the below and return
        }
        damageAmount += 10; //Add 10+ damage to melee attack of player
        PauseController.XPMoney -= upgradeCost; //Take upgrade cost amount off of players current XP       
    }     

    void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position); //Play audio file from FMOD file location
    }   
}