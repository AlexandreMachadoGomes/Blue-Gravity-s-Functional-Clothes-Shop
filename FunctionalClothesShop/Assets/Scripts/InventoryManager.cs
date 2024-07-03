using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public PlayerController player;


    public GameObject itemButtonPrefab;

    public GameObject invBackground;
    public GameObject invUI;

    public Transform itemPanel;






    public void EquipClothes(ClothesData clothes)
    {
        PopulateInventory(clothes.slotType);

        for (int i = 0; i < player.clothesSlotsData.Count; i++)
        {
            if (player.clothesSlotsData[i].slotType == clothes.slotType)
            {
                player.clothesSlotsData[i].ChangeCurrentClothesSlot(clothes);
                return;
            }
        }
    }

    public void UnequipClothes(ClothesData clothes)
    {
        for (int i = 0; i < player.clothesSlotsData.Count; i++)
        {
            if (player.clothesSlotsData[i].slotType == clothes.slotType)
            {
                
            }
        }
    }


    private void PopulateInventory(SlotTypes slotType)
    {
        CleanItemsFromShop();

        GameObject buttonObj;


        //checks if the player already has the item the shop will be displaying and hijacks the behaviour


        for (int j = 0; j < player.clothesSlotsData.Count; j++)
        {
            if (player.clothesSlotsData[j].slotType == slotType)
            { 
                foreach (var item in player.clothesSlotsData[j].availableClothing)
                {
                    if (player.clothesSlotsData[j].currentClothes == item)
                    {
                        buttonObj = Instantiate(itemButtonPrefab, itemPanel);
                        buttonObj.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
                        buttonObj.transform.GetChild(0).GetComponent<Image>().color = item.clothes.GetComponent<SpriteRenderer>().color;
                        buttonObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.goldCost + "  Gold";
                        buttonObj.GetComponentInChildren<Button>().onClick.AddListener(() => UnequipClothes(item));
                        buttonObj.transform.GetChild(2).gameObject.SetActive(true);

                    }
                    else
                    {
                        buttonObj = Instantiate(itemButtonPrefab, itemPanel);
                        buttonObj.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
                        buttonObj.transform.GetChild(0).GetComponent<Image>().color = item.clothes.GetComponent<SpriteRenderer>().color;
                        buttonObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.goldCost + "  Gold";

                        buttonObj.GetComponentInChildren<Button>().onClick.AddListener(() => EquipClothes(item));
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


    public void OpenInventory()
    {
        invBackground.SetActive(true);
        invUI.SetActive(true);
    }

    private void CleanItemsFromShop()
    {
        for (int i = 0; i < itemPanel.childCount; i++)
        {
            Destroy(itemPanel.GetChild(i).gameObject);
        }
    }

    public void CloseInventory()
    {
        CleanItemsFromShop();
        invBackground.SetActive(false);
        invUI.SetActive(false);
    }


}
