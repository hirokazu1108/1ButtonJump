using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBlock : MonoBehaviour
{

    [SerializeField] private Vector3[] distinationPos;
    [SerializeField] float speed;
    private int distinationNum = 1;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, distinationPos[distinationNum], speed * Time.deltaTime);

        if (Vector3.Distance(transform.position,distinationPos[distinationNum])<1/speed)
        {
            distinationNum = (distinationNum +1)% distinationPos.Length;
        }
    }
}
