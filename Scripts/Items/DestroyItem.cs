using UnityEngine;
using System.Collections;

public class DestroyItem : MonoBehaviour {

    public float destroyTime = 600; //Item will be destroy for 10 minutes
	
	void Start () {
        Destroy(gameObject, destroyTime); //Destroy game object after 10 minutes
	}		
}
