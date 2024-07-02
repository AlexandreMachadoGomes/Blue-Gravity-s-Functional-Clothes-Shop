using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{

    public ClothesInventory clothesCollection;

    public override void Interact()
    {

        //UI






        playerController.EndInteractablePause();
    }

    public void BuyClothes()
    {

    }

    public void SellClothes()
    {

    }
}
