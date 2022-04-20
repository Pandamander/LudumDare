using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public bool canStillMove;
    [SerializeField] private Transform startPosition;

    // Update is called once per frame

    private void Start()
    {
        ResetPlayer();
    }

    public void ResetPlayer()
    {
        canStillMove = true;
        gameObject.transform.position = startPosition.position;
        playerMovement.StopVehicle();
    }

    public void Stop()
    {
        canStillMove = false;
        playerMovement.StopVehicle();
    }

    void Update()
    {
        if (canStillMove) // Can't move if you've been caught
        {
            playerMovement.SetInput(
                new Vector2(
                    Input.GetAxis("Horizontal"),
                    Input.GetAxis("Vertical")
                )
            );
        }
    }
}
