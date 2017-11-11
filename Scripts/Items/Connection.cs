using UnityEngine;
using System.Collections;

public class Connection : MonoBehaviour {

    //Script used for connecting scripts

    ItemRespawn ir; //Reference to Item Respawn
    public bool connectioner; //Connectioner set to a bool
   
    void Start ()
    {
        //Setting up references
        ir = GetComponent<ItemRespawn>(); 
	}	

    void Item()
    {
        if(ir.triggered == true) //If triggered in Item Respawn is true then...
        {
            connectioner = true; //Connectioner is true...
        }
        
    }
}
