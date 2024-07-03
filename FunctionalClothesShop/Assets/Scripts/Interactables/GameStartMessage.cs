using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartMessage : DialogueInteractable
{
    public override void FinishedDialogue()
    {
        playerController.EndInteractablePause();
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override void StartedDialogue()
    {
        playerController.StartInteractablePause();
        dialogueManager.ManageDialogue(dialogue, this, isCatDialogue);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("StartedDialogue", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
