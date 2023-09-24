using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageManager", menuName = "ScriptableObjects/CreateStageManager")]
public class StageManager : ScriptableObject
{
    public static int stageNum = 2;
    public static int selectStageNum;
    [SerializeField] GameObject[] stage;
    [SerializeField] Vector3[] spawnPoint;

    //�X�e�[�W���擾
    public GameObject getStageObject()
    {
        return stage[selectStageNum];
    }

    //�X�|�[���ꏊ���擾
    public Vector3 getSpawnPoint()
    {
        return spawnPoint[selectStageNum];
    }
}
