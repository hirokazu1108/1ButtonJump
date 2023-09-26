using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;

    [SerializeField] private GameObject clearShow;
    [SerializeField] private GameObject failedShow;
    [SerializeField] private Text endStateText;
    [SerializeField] private Text stageTitleText;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text clearTimeTexts;
    [SerializeField] private Transform starsTransform;



    void Start()
    {
        switch (GameController.endState) {
            case EndState.Timeover:
                clearShow.SetActive(false);
                failedShow.SetActive(true);
                endStateText.text = "TIME OVER";
                break;
            case EndState.Death:
                clearShow.SetActive(false);
                failedShow.SetActive(true);
                endStateText.text = "GAME OVER";
                break;
            case EndState.Clear:
                clearShow.SetActive(true);
                failedShow.SetActive(false);
                endStateText.text = "";
                var time = Mathf.FloorToInt(GameController.maxTime - GameController.gameTime);
                var sum_score = Mathf.FloorToInt(GameController.gameTime * 25 + GameController.starNum * 1000);  //総合スコア
                var sendNum = StageManager.selectStageNum + 1;
                var sendData = (float)Mathf.FloorToInt(sum_score);
                UnityroomApiClient.Instance.SendScore(sendNum, sendData, ScoreboardWriteMode.HighScoreAsc);
                scoreText.text = sum_score.ToString("00000");
                clearTimeTexts.text = time + "秒";
                break;
            default:
                endStateText.text = "";
                break;
        }

        stageTitleText.text = "ステージ " + (StageManager.selectStageNum+1).ToString("0");
        //持っている星の数だけ黄色い星にする
        for (int starItr = 0; starItr < GameController.starNum; starItr++)
        {
            starsTransform.GetChild(starItr).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

    }

}
