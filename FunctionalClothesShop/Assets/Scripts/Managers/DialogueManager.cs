using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueUI;
    public GameObject textUI;
    public TextMeshProUGUI dialogueText;

    public float delayBetweenLetters = 0.1f;

    private int totalDialoguePhrases;
    private int curentDialoguePhraseIndex = 0;

    private bool isDialoguing = false;
    private SingleDialaogue currentDialogue;
    private DialogueInteractable currentDialogueData;
    private bool finishedCurrentDialogue = true;

    private bool isCatDialogue = false;
    public GameObject catIcon;
    private Animator catAnimator;

    private bool displayTextInstantly = false;

    private Coroutine textCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        catAnimator = catIcon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialoguing && !finishedCurrentDialogue)
        {
            CheckForInputs();
        }
    }


    public void ManageDialogue(SingleDialaogue dialogue, DialogueInteractable dialogueData, bool catDialogue)
    {
        currentDialogueData = dialogueData;
        isDialoguing = true;
        finishedCurrentDialogue = false;
        currentDialogue = dialogue;
        isCatDialogue = catDialogue;

        if (catDialogue)
        {
            catIcon.SetActive(true);
        }

        dialogueUI.SetActive(true);
        textUI.SetActive(true);

        totalDialoguePhrases = dialogue.dialogue.Count;


        textCoroutine = StartCoroutine(DisplayTextLetterByLetter(dialogue.dialogue[0], dialogue.endPhrasePauseTime[0]));
        curentDialoguePhraseIndex++;

    }

    private void CheckForInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!finishedCurrentDialogue)
            {
                TriggerInstantDisplay();
            }
            else
            {
                FinishDialogue();
            }
        }

    }


    private void FinishDialogue()
    {
        curentDialoguePhraseIndex = 0;

        if (isCatDialogue)
        {
            catIcon.SetActive(false);
        }

        dialogueUI.SetActive(false);
        textUI.SetActive(false);

        finishedCurrentDialogue = false;
        isDialoguing = false;
        currentDialogueData.FinishedDialogue();

        isCatDialogue = false;
        currentDialogueData = null;
        currentDialogue = null;
    }


    public void TriggerInstantDisplay()
    {
        displayTextInstantly = true;
    }


    IEnumerator DisplayTextLetterByLetter(string text, float delayAfterPhrase)
    {
        if (isCatDialogue)
        {
            catAnimator.SetBool("isTalking", true);
        }


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

        if (isCatDialogue)
        {
            catAnimator.SetBool("isTalking", false);
        }

        yield return new WaitForSeconds(delayAfterPhrase);

        if (curentDialoguePhraseIndex < totalDialoguePhrases)
        {

            textCoroutine = StartCoroutine(DisplayTextLetterByLetter(currentDialogue.dialogue[curentDialoguePhraseIndex], currentDialogue.endPhrasePauseTime[curentDialoguePhraseIndex]));
            curentDialoguePhraseIndex++;
        }
        else
        {
            FinishDialogue();
            finishedCurrentDialogue = true;
        }

    }



}
