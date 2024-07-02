using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



//Update this enum when creating new clothing slot types.
public enum SlotTypes { ROBE, HAT, HAIR };


//It is necessary to confire new ClothesData Scriptable Object instances for each new clothing created.
//Each clothing should then be stored inside a ClothesIventory Scriptable Object instance for the new slot and that Inventory to be stored here in the manager.
public class ClothesTypeDataManager : MonoBehaviour
{

    [System.NonSerialized]
    public int currentClothesIndex = 0;
    public ClothesInventory clothesInventory;
    public List<ClothesData> availableClothing;
    public GameObject currentClothes;
    [System.NonSerialized]
    public Animator clothesAnimator;
    [System.NonSerialized]
    public SpriteRenderer clothesSpriteRenderer;

    public Transform player;
    public SlotTypes slotType;





    void Start()
    {
        availableClothing = clothesInventory.Clothes;

        currentClothesIndex = clothesInventory.currentClothesIndex;
        ChangeCurrentClothesSlot(currentClothesIndex);
    }

    

    //Adds new clothes to this slots inventory
    public void AddClothes(ClothesData clothes)
    {
        availableClothing.Add(clothes);
    }

    //destroys the previous instance of clothing of its slot and creates a new instance of the new clothing
    public void ChangeCurrentClothesSlot(int index)
    {
        if (index <= availableClothing.Count)
        {
            if (currentClothes != null)
            {
                Destroy(currentClothes);
            }
            currentClothes = null;

            currentClothesIndex = index;

            currentClothes = Instantiate(availableClothing[index].clothes, player.position + availableClothing[index].clothesSpriteOffset, Quaternion.identity);
            currentClothes.transform.parent = player;
            clothesAnimator = currentClothes.GetComponent<Animator>();
            clothesSpriteRenderer = currentClothes.GetComponent<SpriteRenderer>();

        }
    }


    //saves the inventory and equipped clothing to a Scriptable Object when the session ends, so that it persists in between sessions 
    public void ExitGameCleanup()
    {
        clothesInventory.Clothes = availableClothing;
        clothesInventory.currentClothesIndex = currentClothesIndex;
        EditorUtility.SetDirty(clothesInventory);
        AssetDatabase.SaveAssets();

    }


}
