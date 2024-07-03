using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteractable : Interactable
{

    public ShopManager shopManager;
    public ClothesData clothes;

    private GameObject decorationClothes;

    public float upDownTime;

    public Coroutine changeMovingDirection;
    private bool movingUp = true;
    public float movingSpeed = 1;

    public float randomStartTimeMax = 0.5f;

    public float offsetY = 0.2f;


    public override void Interact()
    {

        //checks if the player already has the item the shop will be displaying and hijacks the behaviour
        for (int j = 0; j < playerController.clothesSlotsData.Count; j++)
        {
            if (playerController.clothesSlotsData[j].slotType == clothes.slotType)
            {
                if (playerController.clothesSlotsData[j].availableClothing.Count == 0)
                {
                    shopManager.OpenPopup(true, clothes, true);
                }
                for (int k = 0; k < playerController.clothesSlotsData[j].availableClothing.Count; k++)
                {
                    if (playerController.clothesSlotsData[j].availableClothing[k] == clothes)
                    {
                        shopManager.OpenPopup(false, clothes, true);
                        
                    }
                    else
                    {
                        shopManager.OpenPopup(true, clothes, true);
                    }
                }
            }
        }
    }



    private IEnumerator ChangeMovingDirection()
    {
        yield return new WaitForSeconds(upDownTime);
        movingUp = !movingUp;
        changeMovingDirection = StartCoroutine("ChangeMovingDirection");
    }

    private void MoveDecorationClothes()
    {
        if (decorationClothes)
        {
            int movingDir = movingUp ? 1 : -1;
            decorationClothes.transform.Translate(Vector2.up * movingSpeed * 0.01f * movingDir);
        }
    }

    public void Update()
    {
        MoveDecorationClothes();
    }

    private IEnumerator randomStartTime()
    {
        yield return new WaitForSeconds(Random.Range(0, randomStartTimeMax));
        changeMovingDirection = StartCoroutine("ChangeMovingDirection");
    }

    public void Start()
    {
        decorationClothes = Instantiate(clothes.clothes, transform);
        decorationClothes.transform.position = new Vector2(decorationClothes.transform.position.x, decorationClothes.transform.position.y  - offsetY);
        decorationClothes.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;

        changeMovingDirection = StartCoroutine("randomStartTime");
    }
}
