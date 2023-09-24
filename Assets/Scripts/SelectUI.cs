using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    [SerializeField] private UserData userData;
    [SerializeField] private Transform[] starsTransform;
    [SerializeField] private Text[] hiScoreTexts;
    [SerializeField] private Text[] clearTimeTexts;

    void Start()
    {
        for(int stageItr=0; stageItr < StageManager.stageNum; stageItr++)
        {
            hiScoreTexts[stageItr].text = userData.hiScore[stageItr].ToString("00000");
            clearTimeTexts[stageItr].text = userData.clearTime[stageItr].ToString("000")+"•b";

            //Ž‚Á‚Ä‚¢‚é¯‚Ì”‚¾‚¯‰©F‚¢¯‚É‚·‚é
            for(int starItr = 0; starItr < userData.starNum[stageItr]; starItr++)
            {
                starsTransform[stageItr].GetChild(starItr).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            
        }
    }
}
