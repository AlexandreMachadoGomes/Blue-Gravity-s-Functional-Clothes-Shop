using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Each new clothing in the shop should have a button that calls 
public class Shopkeeper : Interactable
{

    public ClothesInventory clothesCollection;

    public PlayerController player;

    public override void Interact()
    {

        //UI






        playerController.EndInteractablePause();
    }



    public void BuyClothes(ClothesData Clothes)
    {
        player.AddClothes(Clothes);
    }

    public void SellClothes()
    {

    }
}
