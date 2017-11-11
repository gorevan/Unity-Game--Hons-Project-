using UnityEngine;
using System.Collections;

public class AnimatorManager : MonoBehaviour {
   
    private Animator anim; //Reference to Animator

        void Start ()
    {
        //Setting up reference
        anim = GetComponent<Animator>();       
    }
		
	void Update () {

        if (Input.GetKey(KeyCode.Mouse0)) //If player presses left mouse button then...
        {
            anim.SetBool("hit", true); //Play punching animation                                                                   
        }
        else 
        {
            anim.SetBool("hit", false); ////Stop punching animation
        }

        if (Input.GetKey(KeyCode.LeftShift)) //If player presses left shift button then...
        {
            anim.SetBool("sprint", true); //Play sprinting animation
        }
        else 
        {
            anim.SetBool("sprint", false); //Stop sprinting animation
        }

        if (Input.GetKey(KeyCode.Space)) //If player presses spacebar then...
        {
            anim.SetBool("jump", true); //Play jumping animation
        }
        else
        {
            anim.SetBool("jump", false); //Stop jumping animation
        }
    }

    void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position); //Play audio file from FMOD file location
    }
}
