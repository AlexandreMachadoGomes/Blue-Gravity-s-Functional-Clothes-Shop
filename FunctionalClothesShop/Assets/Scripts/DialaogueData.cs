using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DialaogueData : Interactable
{



    //Used in order to have different dialogues after a certain trigger
    protected bool triggerDifferentDialogue = false;

    public SingleDialaogue dialogue;
    public SingleDialaogue differentDialogue;

    public bool isCatDialogue = false;

    public abstract void StartedDialogue();
    public abstract void FinishedDialogue();

}
