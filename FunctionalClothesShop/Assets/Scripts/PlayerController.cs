using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private bool isWalking = false;
    private Animator animator;

    private int CurrentClothesIndex = 0;
    private List<ClothesData> availableClothing;
    public ClothesInventory clothesInventory;
    private GameObject currentClothes;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        availableClothing = clothesInventory.Clothes;
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
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

        if (moveDir != Vector2.zero)
        {
            animator.SetBool("isIddle", false);
        }
        else if (moveDir == Vector2.zero)
        {
            animator.SetBool("isIddle", true);
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

            currentClothes = Instantiate(availableClothing[index].clothes, transform.position, Quaternion.identity);
            currentClothes.transform.parent = transform;
        }
    }
}
