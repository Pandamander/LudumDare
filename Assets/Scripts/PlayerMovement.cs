using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Vector2 movement;
    

    [SerializeField] public float moveSpeed = 40f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float maxSpeed = 40f;

    public CharacterController2D controller;
    [SerializeField] private Animator animator;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed; // Left is -1, Right is 1
        movement.y = Input.GetAxisRaw("Vertical") * moveSpeed; 

        // Animate the car, remembering the last direction
        if (movement.x != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", 0);
        }
        
        if (movement.y != 0)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", movement.y);
        }
            

       
        //animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate() // Runs every 0.02 seconds
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); // This is where the max speed gets applied

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.AddForce(new Vector2(movement.x * moveSpeed, movement.y * moveSpeed) * Time.fixedDeltaTime);
    }

    
}

/*
DONE:


NEXT UP:
Make the idle animation work
Make bad guys that chase you
Make obstacles
 */