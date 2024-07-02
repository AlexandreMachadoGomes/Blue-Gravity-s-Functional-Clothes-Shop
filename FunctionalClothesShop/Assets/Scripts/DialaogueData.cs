using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialaogueData", menuName = "ScriptableObjects/DialaogueData", order = 3)]
public abstract class DialaogueData : Interactable
{



    //Used in order to have different dialogues after a certain trigger
    protected bool triggerDifferentDialogue = false;

    public SingleDialaogue dialogue;
    public SingleDialaogue differentDialogue;



    public abstract void StartedDialogue();
    public abstract void FinishedDialogue();

}
