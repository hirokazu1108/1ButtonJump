using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] GameController gameController;
    private GameObject goalCube;


    private void Start()
    {
        goalCube = transform.GetChild(0).gameObject;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameController.finishGame();
    }

    private void FixedUpdate()
    {
        goalCube.transform.Rotate(0, Time.deltaTime*100, 0);
    }
}
