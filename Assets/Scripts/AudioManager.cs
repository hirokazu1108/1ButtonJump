using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioKinds
{
    SE_Boost = 0,   //ç≈ìKâπó Å@0.25
    SE_Goal = 1,    
    SE_Death = 2,
    SE_Drink = 3,
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] AudioSource bgmAudioSource;

    [SerializeField] AudioClip[] audioClips;

    private static bool isSpawned = false;

    private void Start()
    {
        if(!isSpawned)
            DontDestroyOnLoad(this.gameObject);
    }

    public void playSeOneShot(AudioKinds audioKinds)
    {
        seAudioSource.PlayOneShot(audioClips[(int)audioKinds]);
    }
}
