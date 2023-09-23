using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EndState
{
    NotEnd,
    Clear,
    Death,
    Timeover,
}

public class GameController : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;

    [SerializeField] private GameObject playerPrefab;
    private Player player;
    [SerializeField] private Vector3 spawnPos;

    public static float gameTime = 0;

    public static EndState endState = EndState.NotEnd;

    private void Start()
    {
        StartCoroutine(InitGame());
    }

    public IEnumerator InitGame()
    {
        yield return uIManager.showCountdown();

        //ロボットのスポーン処理
        var instance = Instantiate(playerPrefab, spawnPos, new Quaternion(0, 180f, 0, 0));
        player = instance.GetComponent<Player>();
        player.uIManager = uIManager;

        gameTime = 120f;
        endState = EndState.NotEnd;

        yield break;
    }

    private void Update()
    {
        GameTimer();
    }

    private void GameTimer()
    {
        if(gameTime >0) gameTime -= Time.deltaTime;

        //時間切れ
        if (gameTime < 0)
        {
            gameTime = 0;
            endState = EndState.Timeover;
            StartCoroutine(finishGame());
        }

        uIManager.updateUI(UI.TimerText, gameTime);
    }

    public IEnumerator finishGame()
    {
        yield return uIManager.showFinish();
        SceneManager.LoadScene("ResultScene");

        yield break;
    }
}
