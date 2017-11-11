using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

    CharacterController cc; //Reference to character controller
	
	void Start () {
        //Setting up reference
        cc = GetComponent<CharacterController>();
	}
		
	void Update () {
	if(cc.isGrounded == true && cc.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false) //If player is grounded, they are moving and footstep audio is not playing then...
        {
            GetComponent<AudioSource>().volume = Random.Range(0.1f, 0.2f); //Randomise volume within the set range
            GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f); //Randomise pitch within the set range
            GetComponent<AudioSource>().Play(); //Play footstep audio 
        }
	}
}
