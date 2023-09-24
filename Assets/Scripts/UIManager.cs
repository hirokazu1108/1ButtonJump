using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UI
{
    FuelBar,
    TimerText,
    CountdownText,
    BoostBar,
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Text timerText;
    [SerializeField] private Text popupText;

    [SerializeField] private Animator countdownAnim;

    [SerializeField] private GameObject popupPanel;
    [SerializeField] private GameObject boostImage;
    [SerializeField] private Image fuelFillArea;    //fuelSlider‚ÌFillArea

    [SerializeField] private GameObject pausePanel;

    private AudioManager audioManager;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider seSlider;

    private Player player;
    [SerializeField] private SceneChange sceneChange;

    private void Start()
    {
        var p = GameObject.Find("Player(Clone)");
        if (p != null) player = p.GetComponent <Player>();

        var a = GameObject.Find("AudioManager");
        if (a != null) audioManager = a.GetComponent<AudioManager>();
    }

    public void updateUI(UI ui, float value)
    {
        switch (ui) {
            case UI.FuelBar:
                boostImage.SetActive(false);
                fuelFillArea.color = new Color32(146, 155, 255, 255);
                fuelSlider.value = value;
                break;
            case UI.TimerText:
                timerText.text = Mathf.FloorToInt(value).ToString("##0");
                break;
            case UI.BoostBar:
                boostImage.SetActive(true);
                fuelFillArea.color = new Color32(255, 72, 0,255);
                fuelSlider.value = value;
                break;
        }

    }

    public IEnumerator showCountdown()
    {
        popupPanel.SetActive(true);
        popupText.text = "3";
        countdownAnim.SetTrigger("upstart");
        yield return new WaitForSeconds(1f);
        popupText.text = "2";
        countdownAnim.SetTrigger("upstart");
        yield return new WaitForSeconds(1f);
        popupText.text = "1";
        countdownAnim.SetTrigger("upstart");
        yield return new WaitForSeconds(1f);
        popupText.text = "Start";
        countdownAnim.SetTrigger("upstart");
        yield return new WaitForSeconds(1.2f);
        popupText.text = "";
        popupPanel.SetActive(false);

        yield break;
    }

    public IEnumerator showFinish()
    {
        popupPanel.SetActive(true);
        switch (GameController.endState) {
            case EndState.Timeover:
                popupText.text = "TIME UP";
                break;
            case EndState.Death:
                popupText.text = "GAME OVER";
                break;
            case EndState.Clear:
                popupText.text = "CLEAR";
                break;
        }
        yield return new WaitForSeconds(1.5f);
        
        yield break;
    }

    public void pauseButton()
    {
        pausePanel.SetActive(true);
        if (audioManager != null) musicSlider.value = audioManager.bgmAudioSource.volume;
        if (audioManager != null) seSlider.value = audioManager.seAudioSource.volume;
        if (player != null) player.stopMove();
    }

    public void closePausePanel()
    {
        pausePanel.SetActive(false);
        if (player != null) player.reMove();

    }

    public void reStartButton()
    {
        sceneChange.ToGameScene(StageManager.selectStageNum);
    }

    private void Update()
    {
        if (pausePanel.activeSelf)
        {
            if(audioManager != null) audioManager.bgmAudioSource.volume = musicSlider.value;
            if (audioManager != null) audioManager.seAudioSource.volume = seSlider.value;
        }
    }
}
