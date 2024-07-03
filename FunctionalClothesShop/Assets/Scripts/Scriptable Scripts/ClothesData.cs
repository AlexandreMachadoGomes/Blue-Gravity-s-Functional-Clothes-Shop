using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//It is necessary to confire new ClothesData instances for each new clothing created. 
[CreateAssetMenu(fileName = "ClothesData", menuName = "ScriptableObjects/ClothesData", order = 1)]
public class ClothesData : ScriptableObject
{
    public string itemName;
    public SlotTypes slotType;

    public Sprite icon;
    public GameObject clothes;
    public int goldCost;
    
}
