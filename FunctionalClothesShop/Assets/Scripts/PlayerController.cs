using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private int CurrentClothesIndex = 0;
    private List<ClothesData> availableClothing;
    public ClothesInventory clothesInventory;
    private GameObject currentClothes;
    private Animator clothesAnimator;
    Vector3 clothesSpriteOffset = new Vector3(0, -0.138f, 0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        availableClothing = clothesInventory.Clothes;

        CurrentClothesIndex = clothesInventory.currentClothesIndex;
        currentClothes = Instantiate(availableClothing[CurrentClothesIndex].clothes, transform.position + clothesSpriteOffset, Quaternion.identity);
        currentClothes.transform.parent = transform;

        clothesAnimator = currentClothes.GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
        UpdateSpriteLayer();
    }


    private void DetectInput()
    {



        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerMovement(Vector2.up);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerMovement(Vector2.left);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerMovement(Vector2.right);

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerMovement(Vector2.down);

        }
        else if (!Input.anyKeyDown)
        {
            PlayerMovement(Vector3.zero);
        }
    }



    private void PlayerMovement(Vector2 moveDir)
    {
        animator.SetFloat("MoveX", moveDir.x);
        animator.SetFloat("MoveY", moveDir.y);
        clothesAnimator.SetFloat("MoveX", moveDir.x);
        clothesAnimator.SetFloat("MoveY", moveDir.y);

        if (moveDir != Vector2.zero)
        {
            animator.SetBool("isIddle", false);
            clothesAnimator.SetBool("isIddle", false);
            rigidBody.velocity = moveDir;
        }
        else if (moveDir == Vector2.zero)
        {
            rigidBody.velocity = Vector2.zero;
            animator.SetBool("isIddle", true);
            clothesAnimator.SetBool("isIddle", true);
        }
    }

    public void AddClothes(ClothesData clothes)
    {
        availableClothing.Add(clothes);
    }

    public void ChangeClothes(int index)
    {
        if (index <= availableClothing.Count)
        {
            Destroy(currentClothes);
            currentClothes = null;

            CurrentClothesIndex = index;

            currentClothes = Instantiate(availableClothing[index].clothes, transform.position + clothesSpriteOffset, Quaternion.identity);
            currentClothes.transform.parent = transform;
            clothesAnimator = currentClothes.GetComponent<Animator>();
        }
    }

    private void UpdateSpriteLayer()
    {
        spriteRenderer.sortingOrder =  Mathf.RoundToInt(700 - 100 * transform.position.y) ;
    }
}
