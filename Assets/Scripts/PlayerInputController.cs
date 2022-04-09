using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        playerMovement.SetInput(
            new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            )
        );
    }
}
