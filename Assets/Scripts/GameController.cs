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

    public static float gameTime = 0;

    public static EndState endState = EndState.NotEnd;
    public static bool canMove; //プレイヤーが動けるかのフラグ

    [SerializeField] private StageManager stageManager; 

    private void Start()
    {
        StartCoroutine(InitGame());
    }

    public IEnumerator InitGame()
    {
        canMove = false;

        //ステージ読み込み処理
        var stage = Instantiate(stageManager.getStageObject());
        //ロボットのスポーン処理
        var p = Instantiate(playerPrefab, stageManager.getSpawnPoint(), new Quaternion(0, 180f, 0, 0));
        player = p.GetComponent<Player>();
        player.uIManager = uIManager;
        player.gameController = this;
        Camera.main.transform.position = new Vector3(p.transform.position.x, p.transform.position.y + 1, -15f);

        yield return uIManager.showCountdown();

        gameTime = 120f;
        endState = EndState.NotEnd;
        canMove = true;

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
        player.stopMove();
        yield return uIManager.showFinish();
        SceneManager.LoadScene("ResultScene");

        yield break;
    }
}
