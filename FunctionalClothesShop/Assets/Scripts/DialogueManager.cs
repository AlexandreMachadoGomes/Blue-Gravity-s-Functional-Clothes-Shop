using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueUI;
    public GameObject textUI;
    public TextMeshPro dialogueText;

    public float delayBetweenLetters = 0.1f;

    private int totalDialoguePhrases;
    private int curentDialoguePhraseIndex = 0;

    private bool isDialoguing = false;
    private SingleDialaogue currentDialogue;
    private DialaogueData currentDialogueData;
    private bool finishedCurrentDialogue = true;

    private bool displayTextInstantly = false;

    private Coroutine textCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialoguing)
        {
            CheckForInputs();
        }
    }


    public void ManageDialogue(SingleDialaogue dialogue, DialaogueData dialogueData)
    {
        currentDialogueData = dialogueData;
        isDialoguing = true;
        finishedCurrentDialogue = false;
        currentDialogue = dialogue;

        dialogueUI.SetActive(true);
        textUI.SetActive(true);

        totalDialoguePhrases = dialogue.dialogue.Count;


        textCoroutine = StartCoroutine(DisplayTextLetterByLetter(dialogue.dialogue[0], dialogue.endPhrasePauseTime[0]));
        curentDialoguePhraseIndex++;

    }

    private void CheckForInputs()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!finishedCurrentDialogue)
            {
                TriggerInstantDisplay();
            }
            else
            {
                
            }
        }

    }


    private void FinishDialogue()
    {
        currentDialogueData = null;
        currentDialogue = null;

        dialogueUI.SetActive(false);
        textUI.SetActive(false);

        finishedCurrentDialogue = false;
        isDialoguing = false;
        currentDialogueData.FinishedDialogue();
    }


    public void TriggerInstantDisplay()
    {
        displayTextInstantly = true;
    }


    IEnumerator DisplayTextLetterByLetter(string text, float delayAfterPhrase)
    {
        dialogueText.text = "";  // Clear existing text
        foreach (char letter in text.ToCharArray())  // Iterate over each character
        {

            if (displayTextInstantly)
            {
                displayTextInstantly = false;
                dialogueText.text += new string(text.ToCharArray(), dialogueText.text.Length, text.Length - dialogueText.text.Length);
                break;  // Exit the loop and end the coroutine
            }

            dialogueText.text += letter;  // Add each letter to the text
            yield return new WaitForSeconds(delayBetweenLetters);  // Wait before appending next letter
        }

        yield return new WaitForSeconds(delayAfterPhrase);

        if (curentDialoguePhraseIndex < totalDialoguePhrases)
        {

            textCoroutine = StartCoroutine(DisplayTextLetterByLetter(currentDialogue.dialogue[curentDialoguePhraseIndex], currentDialogue.endPhrasePauseTime[curentDialoguePhraseIndex]));
            curentDialoguePhraseIndex++;
        }
        else
        {
            finishedCurrentDialogue = true;
        }
    }



}
