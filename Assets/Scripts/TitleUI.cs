using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider seSlider;

    private void Start()
    {
        audioManager = GameObject.FindObjectsOfType<AudioManager>()[0];
        musicSlider.value = audioManager.bgmAudioSource.volume;
        seSlider.value = audioManager.seAudioSource.volume;
    }
    private void Update()
    {
        changedMusicSlider();
        changedSeSlider();
    }
    public void changedMusicSlider()
    {
        audioManager.bgmAudioSource.volume = musicSlider.value;
    }

    public void changedSeSlider()
    {
        audioManager.seAudioSource.volume = seSlider.value;
    }

}


