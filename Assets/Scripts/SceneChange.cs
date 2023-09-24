using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;

    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToGameScene(int num)
    {
        stageManager.selectStageNum = num;
        SceneManager.LoadScene("GameScene");
    }
    public void ToSelectScene()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
