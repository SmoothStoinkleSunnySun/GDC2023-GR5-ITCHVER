using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [TextArea][SerializeField] string notes;

    [Header("Public")]
    public bool AllowMovement; //this variable decides whether or not the player can move

    //not visible in inspector

    Vector3 moveD; //moveDirection
    float moveX;
    float moveY;
    bool grounded;

    // Update is called once per frame
    void Update()
    {
        if (AllowMovement)
        {
            ProcessInputs();
        }
    }

    private void FixedUpdate()
    {
        //If allowmovement is true/enabled
        if (AllowMovement)
        {
            //actually move method here :)
        }
    }
    private void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        //this if statement prevents the character from going back to whatever animation is the default one when standing still
        // || and && operators explanation: https://kodify.net/csharp/if-else/if-logical-operators/
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            // Animate();
        }
    }
}
