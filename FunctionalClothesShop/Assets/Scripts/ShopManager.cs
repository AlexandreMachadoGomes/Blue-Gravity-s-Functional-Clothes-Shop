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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BuyClothes(ClothesData Clothes)
    {
        shopkeeper.BuyClothes(Clothes);
    }

    public void SellClothes(ClothesData Clothes)
    {

    }



    private void PopulateInventory(SlotTypes slotType)
    {
        CleanItemsFromShop();

        GameObject buttonObj;

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
                                    buttonObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.goldCost/2 +  "  Gold";
                                    buttonObj.GetComponentInChildren<Button>().onClick.AddListener(() => SellClothes(item));

                                    alreadyHaveTheClothes = true;
                                }
                            }
                        }     
                    }
                    if (!alreadyHaveTheClothes)
                    {
                        buttonObj = Instantiate(itemButtonPrefab, itemPanel);
                        buttonObj.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
                        buttonObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.goldCost + "  Gold";

                        buttonObj.GetComponentInChildren<Button>().onClick.AddListener(() => BuyClothes(item));
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


    public void OpenShop()
    {
        shopBackground.SetActive(true);
        shopUI.SetActive(true);
    }

    private void CleanItemsFromShop()
    {
        for (int i = 0; i < itemPanel.childCount; i++)
        {
            Destroy(itemPanel.GetChild(0));
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
