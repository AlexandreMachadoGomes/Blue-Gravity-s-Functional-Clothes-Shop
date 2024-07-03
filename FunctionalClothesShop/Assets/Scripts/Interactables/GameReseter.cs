using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReseter : Interactable
{

    public GameObject popupReset;

    public List<ClothesInventory> currentInventories;
    public List<ClothesInventory> starterInventories;
    public Gold gold;

    public float starterGold = 150;

    

    public override void Interact()
    {
        popupReset.SetActive(true);
    }

    public void ClosePopup()
    {
        popupReset.SetActive(false);
        playerController.EndInteractablePause();
    }

    public void ResetGame()
    {
        gold.goldAmmount = starterGold;
        for (int i = 0; i < currentInventories.Count; i++)
        {
            currentInventories[i].Clothes = starterInventories[i].Clothes;
        }
        ClosePopup();
    }

 

}
