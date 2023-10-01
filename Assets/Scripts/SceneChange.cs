using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;

    public static string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToGameScene(int num)
    {
        StageManager.selectStageNum = num;
        SceneManager.LoadScene("GameScene");
    }
    public void ToSelectScene()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
