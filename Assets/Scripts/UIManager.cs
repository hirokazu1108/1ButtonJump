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

    public void updateUI(UI ui, float value)
    {
        switch (ui) {
            case UI.FuelBar:
                fuelSlider.value = value;
                break;
            case UI.TimerText:
                timerText.text = Mathf.FloorToInt(value).ToString("##0");
                break;
            case UI.BoostBar:
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
}
