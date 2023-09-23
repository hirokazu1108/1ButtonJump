using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
