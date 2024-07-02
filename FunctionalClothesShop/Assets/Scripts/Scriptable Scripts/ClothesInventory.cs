using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ClothesInventory", menuName = "ScriptableObjects/ClothesInventory", order = 2)]
public class ClothesInventory : ScriptableObject
{

    public SlotTypes slotType;
    public List<ClothesData> Clothes;
    public int currentClothesIndex = 0;

}