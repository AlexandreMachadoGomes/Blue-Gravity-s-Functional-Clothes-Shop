using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }


    private void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        if (transform.position.x > 0.2f)
        {
            transform.position = new Vector3(0.2f, transform.position.y, -10);
        }
        if (transform.position.x < -0.35f)
        {
            transform.position = new Vector3(-0.35f, transform.position.y, -10);
        }
        if (transform.position.y > 3.4f)
        {
            transform.position = new Vector3(transform.position.x, 3.4f, -10);
        }
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, -10);
        }
    }
}
