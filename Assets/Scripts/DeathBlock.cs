using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    [SerializeField] GameController gameController;
    private void OnCollisionEnter(Collision collision)
    {
        GameController.endState = EndState.Death;
        StartCoroutine(gameController.finishGame());
    }
}
