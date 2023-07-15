using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //some code from https://www.youtube.com/watch?v=f473C43s8nE
    [TextArea][SerializeField] string notes;

    [Header("Public")]
    [HideInInspector] public bool AllowMovement = true; //this variable decides whether or not the player can move
    public float groundDrag;
    public float playerHeight;
    public LayerMask Ground;

    [Header("Private")]
    [SerializeField] float moveSpeed;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody rb;

    //not visible in inspector

    Vector3 moveD; //moveDirection
    float moveX;
    float moveY;
    bool grounded;

    // Update is called once per frame
    void Update()
    {
        //check if on ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        if (AllowMovement)
        {
            ProcessInputs();
            SpeedControl();
        }

        //change drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

    }

    private void FixedUpdate()
    {
        //If allowmovement is true/enabled
        if (AllowMovement)
        {
            Move();
        }
    }
    void Move()
    {
        moveD = new Vector3(-moveX, 0, moveY).normalized;
        rb.AddForce(10f * moveSpeed * moveD, ForceMode.Force);
    }
    private void ProcessInputs()
    {
        moveY = Input.GetAxisRaw("Horizontal");
        moveX = Input.GetAxisRaw("Vertical");

        //this if statement prevents the character from going back to whatever animation is the default one when standing still
        // || and && operators explanation: https://kodify.net/csharp/if-else/if-logical-operators/
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
           // Animate();
        }
    }
    void Animate()
    {
        //from https://www.youtube.com/watch?v=nlBwNx-CKLg
        anim.SetFloat("AnimMoveX", moveD.x);
        anim.SetFloat("AnimMoveY", moveD.z);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
