using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioKinds
{
    SE_Boost = 0,   //ç≈ìKâπó Å@0.25
    SE_Goal = 1,    
    SE_Death = 2,
    SE_Drink = 3,
    SE_Star = 4,
    SE_Click = 5,
}

public class AudioManager : MonoBehaviour
{
    public AudioSource seAudioSource;
    public AudioSource bgmAudioSource;

    [SerializeField] AudioClip[] audioClips;

    private static bool isSpawned = false;

    private void Start()
    {
        int numAudioManager = FindObjectsOfType<AudioManager>().Length;
        if (numAudioManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void playSeOneShot(AudioKinds audioKinds)
    {
        seAudioSource.PlayOneShot(audioClips[(int)audioKinds]);
    }
}
