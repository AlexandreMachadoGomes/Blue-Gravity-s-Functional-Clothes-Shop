using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;






public class PlayerController : MonoBehaviour
{
    private enum ColliderDirection { UP, DOWN, RIGHT, LEFT};
    private ColliderDirection lastMovementDir = ColliderDirection.DOWN;
    private BoxCollider2D interactableCollider;

    private bool isInteractionPaused = false;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    //Used to detect if theres anything at the interaction zone only when the players inputs Space
    private bool checkColliderForInteraction = false;

    public Gold goldScriptable;

    public List<ClothesTypeDataManager> clothesSlotsData;


    public float interactTime = 0.1f;

    //Keeps the timer for the interaction period after the player inputs Space
    private Coroutine interactTimer;

    public InventoryManager inventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        interactableCollider = GetComponent<BoxCollider2D>();

        
        


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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            checkColliderForInteraction = true;
            interactTimer = StartCoroutine(InteractionTimer());
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryManager.OpenInventory();
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerMovement(Vector2.up);
            if (lastMovementDir != ColliderDirection.UP)
            {
                lastMovementDir = ColliderDirection.UP;
                interactableCollider.size = new Vector2(.16f, .25f);
                interactableCollider.offset = new Vector2(0, .05f);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerMovement(Vector2.left);
            if (lastMovementDir != ColliderDirection.LEFT)
            {
                lastMovementDir = ColliderDirection.LEFT;
                interactableCollider.size = new Vector2(.25f, .16f);
                interactableCollider.offset = new Vector2(-0.18f, -0.1f);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerMovement(Vector2.right);
            if (lastMovementDir != ColliderDirection.RIGHT)
            {
                lastMovementDir = ColliderDirection.RIGHT;
                interactableCollider.size = new Vector2(.25f, .16f);
                interactableCollider.offset = new Vector2(0.18f, -0.1f);
            }

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerMovement(Vector2.down);
            if (lastMovementDir != ColliderDirection.DOWN)
            {
                lastMovementDir = ColliderDirection.DOWN;
                interactableCollider.size = new Vector2(.16f, .25f);
                interactableCollider.offset = new Vector2(0, -.18f);
            }
        }
        else if (!Input.anyKeyDown)
        {
            PlayerMovement(Vector3.zero);
        }
    }



    public void PlayerMovement(Vector2 moveDir)
    {
        animator.SetFloat("MoveX", moveDir.x);
        animator.SetFloat("MoveY", moveDir.y);

        for (int i = 0; i < clothesSlotsData.Count; i++)
        {
            clothesSlotsData[i].clothesAnimator.SetFloat("MoveX", moveDir.x);
            clothesSlotsData[i].clothesAnimator.SetFloat("MoveY", moveDir.y);
        }
        

        if (moveDir != Vector2.zero)
        {
            SetAnimationBools(false);
            rigidBody.velocity = moveDir;
        }
        else if (moveDir == Vector2.zero)
        {
            rigidBody.velocity = Vector2.zero;
            SetAnimationBools(true);
        }
    }

    private void SetAnimationBools(bool isIddle)
    {
        animator.SetBool("isIddle", isIddle);
        for (int i = 0; i < clothesSlotsData.Count; i++)
        {
            clothesSlotsData[i].clothesAnimator.SetBool("isIddle", isIddle);
        }
    }




    //Adds clothes to the correct slot Manager by comparing their slot type using a enum.  
    public void BuyClothes(ClothesData clothes)
    {
        for (int i = 0; i < clothesSlotsData.Count; i++)
        {
            if (clothesSlotsData[i].slotType == clothes.slotType)
            {
                clothesSlotsData[i].AddClothes(clothes);
                HandleMoney(-clothes.goldCost);
                return;
            }
        }

    }


    public void SellClothes(ClothesData clothes)
    {
        for (int i = 0; i < clothesSlotsData.Count; i++)
        {
            if (clothesSlotsData[i].slotType == clothes.slotType)
            {
                clothesSlotsData[i].RemoveClothes(clothes);
                HandleMoney(+clothes.goldCost/2);
                return;
            }
        }
    }


    private void HandleMoney(float money)
    {
        goldScriptable.goldAmmount += money;
    }

    


    //updates the sorting order of the sprite to include realistic depth
    private void UpdateSpriteLayer()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(700 - 100 * transform.position.y);
        for (int i = 0; i < clothesSlotsData.Count; i++)
        {
            clothesSlotsData[i].clothesSpriteRenderer.sortingOrder = Mathf.RoundToInt(701 +i - 100 * transform.position.y);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (checkColliderForInteraction && collision.CompareTag("Interactable") && !isInteractionPaused)
        {
            collision.GetComponent<Interactable>().Interact();
            isInteractionPaused = true;
            checkColliderForInteraction = false;
            PlayerMovement(Vector3.zero);
        }
    }

    public void EndInteractablePause()
    {
        isInteractionPaused = false;
    }

    private IEnumerator InteractionTimer()
    {
        yield return new WaitForSeconds(interactTime);
        checkColliderForInteraction = false;
    }

    public void GameReset()
    {
        goldScriptable.goldAmmount = 0;
        EditorUtility.SetDirty(goldScriptable);
    }



   
}
