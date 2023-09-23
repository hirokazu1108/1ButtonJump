using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Text endStateText;
  
    void Start()
    {
        switch (GameController.endState) {
            case EndState.Timeover:
                endStateText.text = "TIME OVER";
                break;
            case EndState.Death:
                endStateText.text = "GAME OVER";
                break;
            case EndState.Clear:
                endStateText.text = "CLEAR Žc‚è"+GameController.gameTime.ToString("000")+"•b";
                break;
            default:
                endStateText.text = "";
                break;
        }

    }

}
