using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Each new clothing in the shop should have a button that calls 
public class Shopkeeper : DialogueInteractable
{

    public ClothesInventory clothesCollection;

    public PlayerController player;

    public ShopManager shopManager;

    public SingleDialaogue closingDialog;

    private bool isClosingShop = false;

    public override void Interact()
    {
        

        StartedDialogue();






        playerController.EndInteractablePause();
    }



    public void BuyClothes(ClothesData Clothes)
    {
        player.BuyClothes(Clothes);
    }

    public void SellClothes(ClothesData Clothes)
    {
        player.SellClothes(Clothes);
    }


    public override void StartedDialogue()
    {
        if (!triggerDifferentDialogue)
        {
            dialogueManager.ManageDialogue(dialogue, this, isCatDialogue);
            triggerDifferentDialogue = true;
        }
        else
        {
            dialogueManager.ManageDialogue(differentDialogue, this, isCatDialogue);
        }

    }

    public override void FinishedDialogue()
    {
        if (!isClosingShop)
        {
            shopManager.OpenShop();
        }
        else
        {
            isClosingShop = false;
            CloseInteraction();
        }
    }
    

    public void ExitShop()
    {
        dialogueManager.ManageDialogue(closingDialog, this, isCatDialogue);
        isClosingShop = true;
    }

    public void CloseInteraction()
    {
        playerController.EndInteractablePause();
    }

}
