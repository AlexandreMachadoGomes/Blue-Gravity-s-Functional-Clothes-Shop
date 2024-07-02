using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    private enum ColliderDirection { UP, DOWN, RIGHT, LEFT};
    private ColliderDirection colliderDir = ColliderDirection.DOWN;
    private BoxCollider2D interactableCollider;

    private bool isInteractionPaused = false;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer clothesSpriteRenderer;

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

        interactableCollider = GetComponent<BoxCollider2D>();

        availableClothing = clothesInventory.Clothes;

        CurrentClothesIndex = clothesInventory.currentClothesIndex;
        ChangeClothes(CurrentClothesIndex);


    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteractionPaused)
        {
            DetectInput();
            UpdateSpriteLayer();
        }
    }


    private void DetectInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerMovement(Vector2.up);
            if (colliderDir != ColliderDirection.UP)
            {
                colliderDir = ColliderDirection.UP;
                interactableCollider.size = new Vector2(.16f, .25f);
                interactableCollider.offset = new Vector2(0, .05f);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerMovement(Vector2.left);
            if (colliderDir != ColliderDirection.LEFT)
            {
                colliderDir = ColliderDirection.LEFT;
                interactableCollider.size = new Vector2(.25f, .16f);
                interactableCollider.offset = new Vector2(-0.18f, -0.1f);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerMovement(Vector2.right);
            if (colliderDir != ColliderDirection.RIGHT)
            {
                colliderDir = ColliderDirection.RIGHT;
                interactableCollider.size = new Vector2(.25f, .16f);
                interactableCollider.offset = new Vector2(0.18f, -0.1f);
            }

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerMovement(Vector2.down);
            if (colliderDir != ColliderDirection.DOWN)
            {
                colliderDir = ColliderDirection.DOWN;
                interactableCollider.size = new Vector2(.16f, .25f);
                interactableCollider.offset = new Vector2(0, -.18f);
            }
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
            if (currentClothes != null)
            {
                Destroy(currentClothes);
            }
            currentClothes = null;

            CurrentClothesIndex = index;

            currentClothes = Instantiate(availableClothing[index].clothes, transform.position + clothesSpriteOffset, Quaternion.identity);
            currentClothes.transform.parent = transform;
            clothesAnimator = currentClothes.GetComponent<Animator>();
            clothesSpriteRenderer = currentClothes.GetComponent<SpriteRenderer>();

        }
    }

    private void UpdateSpriteLayer()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(700 - 100 * transform.position.y);
        clothesSpriteRenderer.sortingOrder = Mathf.RoundToInt(701 - 100 * transform.position.y);
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space) && collision.CompareTag("Interactable"))
        {
            collision.GetComponent<Interactable>().Interact();
            isInteractionPaused = true;
        }
    }

    public void EndInteractablePause()
    {
        isInteractionPaused = false;
    }







    public void ExitGameCleanup()
    {
        clothesInventory.Clothes = availableClothing;
        clothesInventory.currentClothesIndex = CurrentClothesIndex;
        EditorUtility.SetDirty(clothesInventory);
        AssetDatabase.SaveAssets();

    }
}
