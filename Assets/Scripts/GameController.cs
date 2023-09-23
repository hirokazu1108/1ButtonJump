using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Start()
    {
        
    }

    public void finishGame()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
