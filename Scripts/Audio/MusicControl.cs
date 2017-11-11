using UnityEngine;
using System.Collections;
using FMODUnity;
using UnityEngine.SceneManagement;

public class MusicControl : MonoBehaviour {

    [FMODUnity.EventRef]
    public string music = "event:/Music/Music";

    FMOD.Studio.EventInstance musicEv;


    void Start()
    {
        
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEv.start();      
    }

       
    public void GameStartedMusic()
    {
        musicEv.setParameterValue("GameStarted", 1f);
        musicEv.setParameterValue("Calm", 0f);
        musicEv.setParameterValue("IsDead", 0f);
        musicEv.setParameterValue("IsAttacking", 0f);
    }

    public void CalmMusic()
    {
        musicEv.setParameterValue("Calm", 1f);
        musicEv.setParameterValue("GameStarted", 0f);
        musicEv.setParameterValue("IsAttacking", 0f);
        musicEv.setParameterValue("IsDead", 0f);
    }

    public void BattleMusic()
    {
        musicEv.setParameterValue("IsAttacking", 1f);
        musicEv.setParameterValue("IsDead", 0f);
        musicEv.setParameterValue("GameStarted", 0f);
        musicEv.setParameterValue("Calm", 0f);

    }

    public void DeathMusic()
    {
        musicEv.setParameterValue("IsDead", 1f);
        musicEv.setParameterValue("GameStarted", 0f);
        musicEv.setParameterValue("IsAttacking", 0f);
        musicEv.setParameterValue("Calm", 0f);
    }
   
}
