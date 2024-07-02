using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Holds a dialogue: a list of phrases in string to be said via dialogue when interacted
[CreateAssetMenu(fileName = "SingleDialaogue", menuName = "ScriptableObjects/SingleDialaogue", order = 4)]
public class SingleDialaogue : ScriptableObject
{

    public List<string> dialogue;

    //The time each phrase stays complete in the screen before starting another one 
    public List<float> endPhrasePauseTime;
}
