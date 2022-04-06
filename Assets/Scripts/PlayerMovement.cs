using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Vector2 movement;
    

    [SerializeField] public float moveSpeed = 40f;
    [SerializeField] private Rigidbody2D rb;

    public CharacterController2D controller;
    [SerializeField] private Animator animator;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed; // Left is -1, Right is 1
        movement.y = Input.GetAxisRaw("Vertical") * moveSpeed; // Left is -1, Right is 1

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    
}

/*
DONE:
Set up blend tree animation
Get car sprite
Make car move

NEXT UP:
Make the idle animation work
Make bad guys that chase you
Make obstacles
 */