using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider seSlider;
    [SerializeField] private GameObject player;
    private Rigidbody rb = null;

    float time = 0;
    float preTime =0;
    int mode = 0;
    [SerializeField] Vector3[] spawnPos; 

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        audioManager = GameObject.FindObjectsOfType<AudioManager>()[0];
        musicSlider.value = audioManager.bgmAudioSource.volume;
        seSlider.value = audioManager.seAudioSource.volume;
    }
    private void Update()
    {
        changedMusicSlider();
        changedSeSlider();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (Mathf.Abs(time-preTime) >0.5f)
        {
            if(mode == 0)
            {
                rb.velocity = new Vector3(Random.Range(1f, 2f), Random.Range(-1f, 2f), Random.Range(-1, 1.5f));
                preTime = time;
            }
            else
            {
                rb.velocity = new Vector3(-Random.Range(1f, 2f), Random.Range(-1f, 2f), Random.Range(-1, 1.5f));
                preTime = time;
            }  
        }

        if(player.transform.position.x > 35)
        {
            mode = 1;
            player.transform.position = spawnPos[1];
            rb.velocity = Vector3.zero;
            player.transform.rotation = Quaternion.Euler(0, -180f, 0);
        }
        if(player.transform.position.x < -35)
        {
            mode = 0;
            player.transform.position = spawnPos[0];
            rb.velocity = Vector3.zero;
            player.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
       
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


