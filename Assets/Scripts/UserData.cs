using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/CreateUserData")]
public class UserData : ScriptableObject
{
    public bool[] isClear;
    public int[] hiScore;
    public int[] clearTime;
    public int[] starNum;
}
