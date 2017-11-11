using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {

    public SoundGroup[] soundGroups; //Reference of an array of sound groups

    Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>(); //Dictonary set up with array of AudioClips

    void Awake()
    {
        foreach (SoundGroup soundGroup in soundGroups) //For each sound group in the sound groups array
        {
            groupDictionary.Add(soundGroup.groupID, soundGroup.group); //Add a sound group name slot and audioclip slot
        }
    }

	public AudioClip GetClipFromName(string name)
    {
        if (groupDictionary.ContainsKey(name)) //If the dictionary slot contains audio clip then...
        {
            AudioClip[] sounds = groupDictionary[name]; //AudioClip will be set to the dictionary name (this is for referencing in other scripts)
            return sounds[Random.Range(0, sounds.Length)]; //Play random sound in array 
            
        }
        return null; //If there is not slots then just do nothing
    }

    [System.Serializable]
    public class SoundGroup //Sound Group set to visible via inspector and consists of...
    {
        public string groupID; //Name of group
        public AudioClip[] group; //A group which can contain audio clips
    }
}
