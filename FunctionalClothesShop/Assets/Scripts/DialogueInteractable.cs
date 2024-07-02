using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class used to create interactable objects that start dialogues when interacted with
public abstract class DialogueInteractable : Interactable
{



    //Used in order to have different dialogues after a certain trigger to be set by the inheriting class
    protected bool triggerDifferentDialogue = false;

    //Different possible dialogues depending on wether the trigger is activated
    public SingleDialaogue dialogue;
    public SingleDialaogue differentDialogue;

    //script that receives the dialogues and plays them along with the dialogue UI
    public DialogueManager dialogueManager;

    public bool isCatDialogue = false;

    public abstract void StartedDialogue();
    public abstract void FinishedDialogue();

}
