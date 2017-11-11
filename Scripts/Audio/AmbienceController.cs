using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AmbienceController : MonoBehaviour {

    public AudioMixerSnapshot forestSnapshot; //Reference to forest snapshot
    public AudioMixerSnapshot oceanSnapshot; //Reference to ocean snapshot
    public AudioMixerSnapshot cricketsSnapshot; //Reference to crickets/night time snapshot

    public float transitionTime = 5f; //How long it will take for appropriate audio to transition
    
              
    void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Crickets")) //If player enters cricket trigger collider then...
        {
            cricketsSnapshot.TransitionTo(transitionTime); //Transition to crickets snapshot and cricket ambience will play

        }
        if (collider.CompareTag("Forest")) //If player enters forest trigger collider then...
        {
            
            forestSnapshot.TransitionTo(transitionTime); //Transition to forest snapshot and forest ambience will play
        }
        if (collider.CompareTag("Ocean")) //If player enters ocean trigger collider then...
        {
            oceanSnapshot.TransitionTo(transitionTime); //Transition to ocean snapshot and ocean ambience will play
        }
    }
}            

