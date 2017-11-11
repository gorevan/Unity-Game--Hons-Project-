using UnityEngine;
using System.Collections;

public class Sprinting : MonoBehaviour {
 
    float stamina1 = 5f, maxStamina = 5f; //Current value of player stamina set to 5, maxStamina of player set to 5

    //Referencing of players movement attributes
    public float walkSpeed, runSpeed; 
    public float walkAirSpeed, runAirSpeed; 
    public float walkBackSpeed, runBackSpeed;
    public float walkFallSpeed, runFallSpeed;
    public float walkGroundSpeed, runGroundSpeed;
    public float walkSideSpeed, runSideSpeed;
       
    CharacterMotor cm; //Reference to CharacterMotor
    Player player; //Reference to Player

    [SerializeField]
    private int upgradeCost = 50; //Cost of upgrading sprinting stats

    public float regenTime = 1f; //How long it takes for sprinting bar to regenerate

    bool isRunning; //isRunning defined as a bool

        void Start ()
    {
        //Setting up references
        cm = gameObject.GetComponent<CharacterMotor>();
        player = gameObject.GetComponent<Player>();
        
        

        walkSpeed = cm.movement.maxForwardSpeed; //walkSpeed set to the CharacterMotor's value of maxForwardSpeed (the max value of how fast player can move in forward direction) 
        runSpeed = cm.movement.maxForwardSpeed * 2; //Doubles the walk speed of the player

        walkAirSpeed = cm.movement.maxAirAcceleration; //walkAirSpeed set to the CharacterMotor's value of maxAirAccelerationSpeed (the max value of how fast player can move in the air) 
        runAirSpeed = cm.movement.maxAirAcceleration * 2; //Doubles air speed of player

        walkBackSpeed = cm.movement.maxBackwardsSpeed; //walkBackSpeed set to the CharacterMotor's value of maxBackwardsSpeed (the max value of how fast player can move backwards) 
        runBackSpeed = cm.movement.maxBackwardsSpeed * 2; //Doubles backwards speed of player

        walkFallSpeed = cm.movement.maxFallSpeed; //walkFallSpeed set to the CharacterMotor's value of maxFallSpeed (the max value of how fast player can fall in the air) 
        runFallSpeed = cm.movement.maxFallSpeed * 2; //Doubles fall speed of player

        walkGroundSpeed = cm.movement.maxGroundAcceleration; //walkGroundSpeed set to the CharacterMotor's value of maxGroundSpeed (the max value of how fast player can move across the ground) 
        runGroundSpeed = cm.movement.maxGroundAcceleration * 2; //Doubles ground speed of player

        walkSideSpeed = cm.movement.maxSidewaysSpeed; //walkSideSpeed set to the CharacterMotor's value of maxSidewaysSpeed (the max value of how fast player can move sidewards) 
        runSideSpeed = cm.movement.maxSidewaysSpeed * 2; //Doubles sideways speed of player

        player.stamina.MaxVal = maxStamina; //Player scripts sprinting max value is set to that of this script
        player.stamina.CurrentVal = stamina1; //Player scripts sprinting current value is set to that of this script
        
    }
	
    public void SetRunning(bool isRunning) 
    {
        this.isRunning = isRunning; //Player is running 
        cm.movement.maxForwardSpeed = isRunning ? runSpeed : walkSpeed; //If running set to runSpeed if not set to walkSpeed 
        cm.movement.maxAirAcceleration = isRunning ? runAirSpeed : walkAirSpeed; //If running set to runAirSpeed if not set to walkAirSpeed
        cm.movement.maxBackwardsSpeed = isRunning ? runBackSpeed : walkBackSpeed; //If running set to runBackSpeed if not set to walkBackSpeed
        cm.movement.maxFallSpeed= isRunning ? runFallSpeed : walkFallSpeed; //If running set to runFallSpeed if not set to walkFallSpeed
        cm.movement.maxGroundAcceleration = isRunning ? runGroundSpeed : walkGroundSpeed; //If running set to runGroundSpeed if not set to walkGroundSpeed
        cm.movement.maxSidewaysSpeed = isRunning ? runSideSpeed : walkSideSpeed; //If running set to runSideSpeed if not set to walkSideSpeed


    }

    void Update()
    {
                      
        if (Input.GetButtonDown("Shift")) //If left shift on keyboard is press then...
        {
            SetRunning(true); //The player will run
        }
        if (isRunning) //If player is running then...
        {
            stamina1 -= Time.deltaTime; //Stamina will be decreased as time passes
            player.stamina.CurrentVal = stamina1;  //Players stamina in player script set to the same current stamina in this script
            

            if (stamina1 <= 0) //If players stamina is less than or equal to 0 then...
            {
                player.stamina.CurrentVal = 0; //Players stamina is set to 0
                
                SetRunning(false); //The player will stop running 
                
                
            }
        }
        else if (stamina1 < maxStamina) //If the current stamina of the player is less than the max stamina then...
        {
            stamina1 += Time.deltaTime * regenTime; //Stamina will be increased as time passes
            player.stamina.CurrentVal += Time.deltaTime * regenTime; //Stamina increased in player script   
        }
        
    }
    public void UpgradeStamina()
    {
        if (PauseController.XPMoney < upgradeCost) //If the current XP of player isn't enough for the upgrade cost then...
        {
            return; //Do not upgrade the below and return
        }
        maxStamina += 1; //Max stamina amount is increased by 1
        player.stamina.MaxVal += 1; //Add max stamina value to player script
        PauseController.XPMoney -= upgradeCost; //Take upgrade cost amount off of players current XP    

        if (stamina1 < maxStamina) //If stamina is less than max stamina when upgrading 
        {
            stamina1 = maxStamina; //Set player stamina to full
            
            player.stamina.CurrentVal = maxStamina; //Set player script stamina to full
        }
    }

    public void UpgradeWalkSpeed()
    {
        if (PauseController.XPMoney < upgradeCost) //If the current XP of player isn't enough for the upgrade cost then...
        {
            return; //Do not upgrade the below and return
        }

        //Upgrade all player movement by 2
        cm.movement.maxForwardSpeed += 2;
        cm.movement.maxAirAcceleration += 2;
        cm.movement.maxFallSpeed += 2;
        cm.movement.maxBackwardsSpeed += 2;
        cm.movement.maxGroundAcceleration += 2;
        cm.movement.maxSidewaysSpeed += 2;
        walkSpeed = cm.movement.maxForwardSpeed;
        runSpeed = cm.movement.maxForwardSpeed * 2;

        PauseController.XPMoney -= upgradeCost; //Take upgrade cost amount off of players current XP    
    }
    public void UpgradeSprintSpeed()
    {
        if (PauseController.XPMoney < upgradeCost) //If the current XP of player isn't enough for the upgrade cost then...
        {
            return; //Do not upgrade the below and return
        }
        runSpeed += 1f; //Increase run speed by 1
        PauseController.XPMoney -= upgradeCost; //Take upgrade cost amount off of players current XP    
    }

    public void UpgradeStaminaRegenTime()
    {
        if (PauseController.XPMoney < upgradeCost) //If the current XP of player isn't enough for the upgrade cost then...
        {
            return; //Do not upgrade the below and return
        }
        regenTime += 0.5f; //Stamina Regeneration time will be increased
        PauseController.XPMoney -= upgradeCost; //Take upgrade cost amount off of players current XP    
    }
}