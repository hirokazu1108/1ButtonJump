using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameObject goalCube;


    private void Start()
    {
        goalCube = transform.GetChild(0).gameObject;    
    }


    private void FixedUpdate()
    {
        goalCube.transform.Rotate(0, Time.deltaTime*100, 0);
    }
}
