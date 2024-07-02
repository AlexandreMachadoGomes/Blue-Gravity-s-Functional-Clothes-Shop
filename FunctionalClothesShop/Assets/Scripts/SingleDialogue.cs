using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SingleDialaogue", menuName = "ScriptableObjects/SingleDialaogue", order = 4)]
public class SingleDialaogue : ScriptableObject
{

    public List<string> dialogue;
    public List<float> endPhrasePauseTime;
}
