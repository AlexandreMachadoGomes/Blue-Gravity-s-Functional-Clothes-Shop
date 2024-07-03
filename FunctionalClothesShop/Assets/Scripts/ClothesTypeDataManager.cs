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

    public ClothesInventory clothesInventory;
    public List<ClothesData> availableClothing;
    public GameObject currentClothes;
    public ClothesData currentClothesData;
    public Animator clothesAnimator;
    [System.NonSerialized]
    public SpriteRenderer clothesSpriteRenderer;

    public Transform player;
    public SlotTypes slotType;

    public ClothesData initialClothes;



    void Start()
    {
        availableClothing = clothesInventory.Clothes;

        ChangeCurrentClothesSlot(initialClothes);

        

        
    }

    

    //Adds new clothes to this slots inventory
    public void AddClothes(ClothesData clothes)
    {
        availableClothing.Add(clothes);
    }


    public void RemoveClothes(ClothesData clothes)
    {
        if (currentClothesData == clothes)
        {
            ChangeCurrentClothesSlot(initialClothes);
        }
        availableClothing.Remove(clothes);
    }

    //destroys the previous instance of clothing of its slot and creates a new instance of the new clothing
    public void ChangeCurrentClothesSlot(ClothesData clothes)
    {

        if (availableClothing.Contains(clothes) || clothes == initialClothes)
        {
            if (currentClothes != null)
            {
                Destroy(currentClothes);
            }
            currentClothes = null;


            currentClothes = Instantiate(clothes.clothes, player.position, Quaternion.identity);
            currentClothesData = clothes;
            currentClothes.transform.parent = player;
            clothesAnimator = currentClothes.GetComponent<Animator>();
            clothesSpriteRenderer = currentClothes.GetComponent<SpriteRenderer>();
            player.GetComponent<PlayerController>().PlayerMovement(Vector2.down);
            player.GetComponent<PlayerController>().PlayerMovement(Vector2.zero);
        }
    }


    //saves the inventory and equipped clothing to a Scriptable Object when the session ends, so that it persists in between sessions 
    public void ExitGameCleanup()
    {
        clothesInventory.Clothes = availableClothing;
        EditorUtility.SetDirty(clothesInventory);
        AssetDatabase.SaveAssets();

    }


}
