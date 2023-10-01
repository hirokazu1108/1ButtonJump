using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    [SerializeField] UserData userData;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        Load();
    }

    public void Load()
    {
        for(int i=0; i<StageManager.stageNum; i++)
        {
            var check = PlayerPrefs.GetInt($"HiScore{i}", -1);
            if (check == -1)
            {
                userData.isClear[i] = false;
                userData.hiScore[i] = 0;
                userData.clearTime[i] = 999;
                userData.starNum[i] = 0;
                Debug.Log("データがありません");
            }
            else
            {
                userData.isClear[i] = true;
                userData.hiScore[i] = PlayerPrefs.GetInt($"HiScore{i}", -1);
                userData.clearTime[i] = PlayerPrefs.GetInt($"ClearTime{i}", -1);
                userData.starNum[i] = PlayerPrefs.GetInt($"StarNum{i}", -1);
                Debug.Log("データが見つかりました");
            }
        }

        
    }

    public void Save(int stageNum = 0)
    {
        var sum_score = Mathf.FloorToInt(GameController.gameTime * 25 + GameController.starNum * 1000);   //総合スコア

        if (userData.hiScore[stageNum] < sum_score)
        {
            userData.isClear[stageNum] = true;
            userData.hiScore[stageNum] = sum_score;
            userData.clearTime[stageNum] = Mathf.FloorToInt(GameController.maxTime - GameController.gameTime);
            userData.starNum[stageNum] = GameController.starNum;

            PlayerPrefs.SetInt($"HiScore{stageNum}", userData.hiScore[stageNum]);
            PlayerPrefs.SetInt($"ClearTime{stageNum}", userData.clearTime[stageNum]);
            PlayerPrefs.SetInt($"StarNum{stageNum}", userData.starNum[stageNum]);
        }
    }
}
