using UnityEngine;
using System.Collections;

public class SpawningItems : MonoBehaviour {

    public Transform[] SpawnPoints; //Reference to array of spawn points
    public GameObject[] Items; //Reference  to array of items

    public float spawnTime = 1.5f; //How long it takes for items to spawn

    void Start ()
    {
        InvokeRepeating("SpawnItems", 0, spawnTime); //Repeatidly spawn items after spawntime reaches 0
	}		
	
    void SpawnItems()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length); //index number of the spawn points array is set randomly

        int objectIndex = Random.Range(0, Items.Length); //index number of the object array is set randomly

        Instantiate(Items[objectIndex], SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation); //Spawn items at set locations
    }
}
