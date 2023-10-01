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
    [SerializeField] private GameObject introducePanel;
    private int page = 0;
    private static bool isFirst = true;
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

        if (isFirst)
        {
            IntroduceButton();
            isFirst = false;
        }
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

    public void IntroduceButton()
    {
        introducePanel.SetActive(true);
        introducePanel.transform.GetChild(0).gameObject.SetActive(false);
        introducePanel.transform.GetChild(1).gameObject.SetActive(true);
        introducePanel.transform.GetChild(2).gameObject.SetActive(true);
        introducePanel.transform.GetChild(3).gameObject.SetActive(false);
    }

    public void nextButton()
    {
        if (page == 0)
        {
            page = 1;
            introducePanel.transform.GetChild(0).gameObject.SetActive(true);
            introducePanel.transform.GetChild(1).gameObject.SetActive(false);
            introducePanel.transform.GetChild(2).gameObject.SetActive(true);
            introducePanel.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (page == 1)
        {
            page = 0;
            introducePanel.SetActive(false);
        }
    }
    public void preButton()
    {
        page = 0;
        introducePanel.transform.GetChild(0).gameObject.SetActive(false);
        introducePanel.transform.GetChild(1).gameObject.SetActive(true);
        introducePanel.transform.GetChild(2).gameObject.SetActive(true);
        introducePanel.transform.GetChild(3).gameObject.SetActive(false);
    }


}


