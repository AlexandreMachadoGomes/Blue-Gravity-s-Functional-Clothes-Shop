using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoor : DialaogueData
{

    public DialogueManager dialogueManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Interact()
    {
        StartedDialogue();
    }


    public override void StartedDialogue()
    {
        if (!triggerDifferentDialogue)
        {
            dialogueManager.ManageDialogue(dialogue, this);
        }
        else
        {
            dialogueManager.ManageDialogue(differentDialogue, this);
        }
        
    }

    public override void FinishedDialogue()
    {
        playerController.EndInteractablePause();
    }
}
