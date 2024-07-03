using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public PlayerController playerController;

    //must call EndInteractablePause() from playerController after finishing interaction to unrestrict the player
    public abstract void Interact();


}
