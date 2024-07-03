using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Great example of how to set up a simple DialogueData interactable
public class SimpleInteractable : DialogueInteractable
{

    public override void Interact()
    {
        StartedDialogue();
    }


    public override void StartedDialogue()
    {
  
        dialogueManager.ManageDialogue(dialogue, this, isCatDialogue);
        
    }

    public override void FinishedDialogue()
    {
        playerController.EndInteractablePause();
    }
}
