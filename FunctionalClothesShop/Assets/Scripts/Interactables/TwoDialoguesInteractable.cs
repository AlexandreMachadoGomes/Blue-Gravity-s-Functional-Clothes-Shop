using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDialoguesInteractable : DialogueInteractable
{
    public override void Interact()
    {
        StartedDialogue();
        triggerDifferentDialogue = true;
    }


    public override void StartedDialogue()
    {
        if (!triggerDifferentDialogue) 
        {
            dialogueManager.ManageDialogue(dialogue, this, isCatDialogue);
        }
        else
        {
            dialogueManager.ManageDialogue(differentDialogue, this, isCatDialogue);
        }
        

    }

    public override void FinishedDialogue()
    {
        playerController.EndInteractablePause();
    }
}
