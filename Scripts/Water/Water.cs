using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

    SkinnedMeshRenderer mr; //Reference to Skinned Mesh Renderer on arms

    void Start()
    {
        //Setting up reference
        mr = GetComponent<SkinnedMeshRenderer>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Water") //If the player enters the waters collider then...
        {
            mr.enabled = false; //Players arms will disappear
        }        
    }
    void OnTriggerExit(Collider collider) 
    {
        if (collider.tag == "Water") //If the player exists the water collider then...
        mr.enabled = true; //Players arms will reappear
    }
}