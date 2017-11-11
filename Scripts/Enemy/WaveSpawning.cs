using UnityEngine;
using System.Collections;


public class WaveSpawning : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING} //Setting up spawn states. 0 = Spawning, 1 = Waiting, 2 = Counting

    [System.Serializable]
	public class Wave //Wave class consists of...
    {
        public string name; //Name of wave set up
        public Transform enemy; //Reference to the transform of the enemy 
        public int count; //Count value set up
        public float rate; //Rate value set up        
    }

    public Wave[] waves; //Reference to array of waves
    private int nextWave = 0; //How long till next wave

    public Transform[] spawnPoints; //Reference to array of spawn points

    public float timeBetweenWaves = 5f; //Time between the waves is set
    private float waveCountdown; //Countdown between waves 

    private float searchCountdown = 1f; //Searching whether player has killed all enemies within current wave

    private SpawnState state = SpawnState.COUNTING; //Setting current spawn state to COUNTING

    

    void Start()
    {
        waveCountdown = timeBetweenWaves; //Wave countdown has the same value of time between waves    
    }

    void Update()
    {
        if( state == SpawnState.WAITING)//If spawn system is waiting...
        {
            if (!EnemyIsAlive()) //If enemies are all dead then...
            {
                WaveCompleted(); //Wave will be completed
                Debug.Log("Wave Completed"); //Display that the wave is complete in console
            }
            else
            {
                return; //If enemy isn't dead then do not end wave
            }
        }
        if(waveCountdown <= 0) //If wave countdown is finished then...
        {
           if(state != SpawnState.SPAWNING) //If the state is not SPAWNING then...
            {
                StartCoroutine(SpawnWave(waves[nextWave])); //Start next wave and then start spawning enemies 
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime; //If wave countdown is not finished then continue to countdown
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!"); //Display Wave Completed within the console
        state = SpawnState.COUNTING; //When wave is completed set state to COUNTING

        waveCountdown = timeBetweenWaves; // Wave countdown is set to time between waves

        if(nextWave + 1 > waves.Length - 1) //If all waves are completed then...
        {
            nextWave = 0; //Start at first wave again
            Debug.Log("All Waves Complete."); //Display All Waves Complete in console
        }
        else
        {
            nextWave++; //If all waves aren't complete then continue to next wave in array
        }              
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime; //Search countdown starts counting down 

        if(searchCountdown <= 0f) //If search countdown is finished then...
        {
            searchCountdown = 1f; //Search will be set to 1
            if (GameObject.FindGameObjectWithTag("Enemy") == null) //If Enemy is dead then...
            {
                return false; //Enemy is not alive
            }
        }       
        return true; //Enemy is alive
    }
    public IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name); //Display Spawning Wave: and the name of the current wave in console
        state = SpawnState.SPAWNING; //Current state set to SPAWNING

        for(int i = 0; i < _wave.count; i++) //For loop for spawning through enemies
        {
            SpawnEnemy(_wave.enemy); //Spawn enemy that is set in wave
            yield return new WaitForSeconds(1f/_wave.rate); //Delay before spawning of each enemy
        }
        state = SpawnState.WAITING; //State is set to WAITING

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {        
        Debug.Log("Spawning Enemy: " + _enemy.name); //Display Spawning Enemy and the enemy type in console

        if(spawnPoints.Length == 0) //If there are no spawn points in array then...
        {
            Debug.LogError("No spawn points assigned."); //Display that there is no spawn points assigned in the console
        }

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)]; //Randomise spawn points of where enemies will spawn
        Instantiate(_enemy,_sp.position, _sp.rotation); //Spawn enemies at the position and rotation of spawn points
    }  
}
