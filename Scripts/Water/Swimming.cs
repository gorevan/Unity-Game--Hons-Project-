using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;
using System.Reflection;
using System;
using System.Collections.Generic;

public class Swimming : MonoBehaviour {
   
    private CharacterController cc; //Reference to Character Controoler
    private CharacterMotor cm; //Reference to Character Motor
            
    void Start()
    {        
        //Setting up references
        cc = GetComponent<CharacterController>();
        cm = GetComponent<CharacterMotor>();                                             
    }

    private void Update()
    {               
        cm.jumping.enabled = !IsUnderwater(); //jumping is enabled if not underwater and is disabled if player is underwater
        if (IsUnderwater()) //If the player is underwater...
        {           
            if (Input.GetKey(KeyCode.Q)) //If player presses Q...
                cm.SetVelocity(new Vector3(cc.velocity.x, 3, cc.velocity.z)); //Player will float to the surface         
        }          
    }
    bool IsUnderwater()
    {
        return gameObject.transform.position.y < 5; //Player is underwater if they are under the local position of 5 on the Y axis
    }
}

