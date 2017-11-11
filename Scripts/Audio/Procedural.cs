using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AudioData
{
    public AudioClip audioClip;

    [Range(0.01f, 1.0f)]
    public float volume;
}

public class Procedural : MonoBehaviour
{
    [Tooltip("List of AudioClip components. This list must be set to the sounds to be played.")]
    public List<AudioData> audioDataList = new List<AudioData>();

    [Tooltip("The main AudioSource component to play the main loop.")]
    public AudioSource mainAudioSource;

    [Tooltip("The main AudioClip loop.")]
    public AudioClip mainAudioClipLoop;

    [Tooltip("The AudioSource component to play the AudioClips.")]
    public AudioSource audioSource;

    [Tooltip("AudioSource default. Only used if AudioSource is created at runtime.")]
    public bool loopClips = false;

    [Tooltip("Start playing sound immediately.")]
    public bool playNow = true;

    void Start()
    {
        //if there is a main audio clip to be looped
        if (mainAudioClipLoop != null)
        {
            //make sure we have a reference to the AudioSource
            if (mainAudioSource == null)
            {
                mainAudioSource = gameObject.AddComponent<AudioSource>();
            }
            //play and loop the main audio clip
            mainAudioSource.loop = true;
            mainAudioSource.clip = mainAudioClipLoop;
            mainAudioSource.Play();
        }

        // make sure we have a reference to the AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        //make sure that alll the AudioClips in the audioDataList are valid by removing any null clips
        audioDataList.RemoveAll(item => item.audioClip == null);

        if (playNow && (audioDataList.Count > 0))
        {
            PlaySequentialSounds();
        }
    }

    public void PlaySequentialSounds()
    {
        StartCoroutine(PlaySounds());
    }

    public void StopSounds()
    {
        StopCoroutine("PlaySounds");
    }

    IEnumerator PlaySounds()
    {
        if (audioDataList.Count > 0)
        {
            //shuffle the list of sounds
            ShuffleSounds();

            for (int i = 0; i < audioDataList.Count; i++)
            {
                //make sure the volume is set
                if (audioDataList[i].volume == 0)
                    audioDataList[i].volume = 0.5f;
                audioSource.volume = audioDataList[i].volume;
                audioSource.PlayOneShot(audioDataList[i].audioClip);

                //wait for the lenght of the clip to finish playing
                yield return new WaitForSeconds(audioDataList[i].audioClip.length);
            }
        }
        yield return null;

        //if loopClips is set to true then call coroutine
        if (loopClips)
        {
            StartCoroutine(PlaySounds());
        }
    }

    //Fisher-Yates Shuffle
    public void ShuffleSounds()
    {
        if (audioDataList.Count > 1)
        {
            for (int i = audioDataList.Count - 1; i > 0; --i)
            {
                int j = (int)UnityEngine.Random.Range(0, i);
                AudioData temp = audioDataList[i];
                audioDataList[i] = audioDataList[j];
                audioDataList[j] = temp;
            }
        }
    }
}
