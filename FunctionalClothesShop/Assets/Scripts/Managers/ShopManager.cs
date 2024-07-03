using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public PlayerController player;


    public GameObject itemButtonPrefab;

    public GameObject shopBackground;
    public GameObject shopUI;
    
    public Transform itemPanel;

    public Shopkeeper shopkeeper;

    public List<ClothesInventory> ClothesToSell;

    public Gold goldScriptable;
    public TextMeshProUGUI goldText;

    public GameObject popupUI;
    private ClothesData popupClothes;
    public bool buyingNotSellingPopup = false;
    private bool itemFromTablePopup = false;

    public Gold gold;



    public void BuyClothes(ClothesData clothes)
    {
        shopkeeper.BuyClothes(clothes);
        
        if (itemFromTablePopup)
        {
            itemFromTablePopup = false;
            player.EndInteractablePause();
        }
        else
        {
            PopulateInventory(clothes.slotType);
        }
    }

    public void SellClothes(ClothesData clothes)
    {
        shopkeeper.SellClothes(clothes);
        if (itemFromTablePopup)
        {
            itemFromTablePopup = false;
            player.EndInteractablePause();
        }
        else
        {
            PopulateInventory(clothes.slotType);
        }
    }



    private void PopulateInventory(SlotTypes slotType)
    {
        CleanItemsFromShop();

        GameObject buttonObj;

        goldText.text = goldScriptable.goldAmmount.ToString();


        //Checks wich slots inventory should be acessed for the items display
        for (int i = 0; i < ClothesToSell.Count; i++)
        {
            if (ClothesToSell[i].slotType == slotType)
            {
                foreach (ClothesData item in ClothesToSell[i].Clothes)
                {
                    //checks if the player already has the item the shop will be displaying and hijacks the behaviour
                    bool alreadyHaveTheClothes = false;
                    for (int j = 0; j < player.clothesSlotsData.Count; j++)
                    {
                        if (player.clothesSlotsData[j].slotType == slotType)
                        {
                            for (int k = 0; k < player.clothesSlotsData[j].availableClothing.Count; k++)
                            {
                                if (player.clothesSlotsData[j].availableClothing[k] == item)
                                {
                                    buttonObj = Instantiate(itemButtonPrefab, itemPanel);
                                    buttonObj.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
                                    buttonObj.transform.GetChild(0).GetComponent<Image>().color = item.clothes.GetComponent<SpriteRenderer>().color;
                                    buttonObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.goldCost/2 +  "  Gold";
                                    buttonObj.GetComponentInChildren<Button>().onClick.AddListener(() => OpenPopup(false, item));
                                    buttonObj.transform.GetChild(2).gameObject.SetActive(true);

                                    alreadyHaveTheClothes = true;
                                }
                            }
                        }     
                    }
                    if (!alreadyHaveTheClothes)
                    {
                        buttonObj = Instantiate(itemButtonPrefab, itemPanel);
                        buttonObj.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
                        buttonObj.transform.GetChild(0).GetComponent<Image>().color = item.clothes.GetComponent<SpriteRenderer>().color;
                        buttonObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.goldCost + "  Gold";

                        buttonObj.GetComponentInChildren<Button>().onClick.AddListener(() => OpenPopup(true, item));
                    }
                    
                }
            }
        }
        
    }


    //IMPORTANT: need to create a new wrapper function for each new slot created, as the button cant send enum as parameters, and sending a int instead could get confusing quickly
    public void RobesCategorySelected()
    {
        PopulateInventory(SlotTypes.ROBE);
    }
    public void HairCategorySelected()
    {
        PopulateInventory(SlotTypes.HAIR);
    }
    public void HatsCategorySelected()
    {
        PopulateInventory(SlotTypes.HAT);
    }


    public void OpenPopup(bool buyingNotSelling, ClothesData clothes)
    {
        buyingNotSellingPopup = buyingNotSelling;
        popupClothes = clothes;

        popupUI.SetActive(true);

        popupUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = buyingNotSelling ? "Buy" : "Sell";
        popupUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = clothes.itemName;
        popupUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gold.goldAmmount.ToString();
        popupUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = (buyingNotSelling ? clothes.goldCost : clothes.goldCost / 2) + " Gold";

    }

    public void OpenPopup(bool buyingNotSelling, ClothesData clothes, bool itemFromTablePopUp)
    {
        itemFromTablePopup = itemFromTablePopUp;

        buyingNotSellingPopup = buyingNotSelling;
        popupClothes = clothes;

        popupUI.SetActive(true);

        popupUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = buyingNotSelling ? "Buy" : "Sell";
        popupUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = clothes.itemName;

        popupUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gold.goldAmmount.ToString();
        popupUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = (buyingNotSelling ? clothes.goldCost : clothes.goldCost/2) + " Gold";


    }

    public void ClosePopupYes()
    {
        if (buyingNotSellingPopup)
        {
            BuyClothes(popupClothes);
        }
        else
        {
            SellClothes(popupClothes);
        }
        popupUI.SetActive(false);
        popupClothes = null;
        buyingNotSellingPopup = false;


    }

    public void ClosePopupNo()
    {
        popupUI.SetActive(false);
        popupClothes = null;
        buyingNotSellingPopup = false;
        if (itemFromTablePopup)
        {
            itemFromTablePopup = false;
            player.EndInteractablePause();
        }

    }

    public void OpenShop()
    {
        shopBackground.SetActive(true);
        shopUI.SetActive(true);
        goldText.text = goldScriptable.goldAmmount.ToString();
    }

    private void CleanItemsFromShop()
    {
        for (int i = 0; i < itemPanel.childCount; i++)
        {
            Destroy(itemPanel.GetChild(i).gameObject);
        } 
    }

    public void CloseShop()
    {
        CleanItemsFromShop();
        shopBackground.SetActive(false);
        shopUI.SetActive(false);
        shopkeeper.ExitShop();
    }


}
