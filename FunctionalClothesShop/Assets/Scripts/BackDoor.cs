using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Great example of how to set up a simple DialogueData interactable
public class BackDoor : DialogueInteractable
{

    public override void Interact()
    {
        StartedDialogue();
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
