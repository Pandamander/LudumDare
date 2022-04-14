using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCaught : MonoBehaviour
{
    [SerializeField] private OnOffUI endingUI;
    private PlayerInputController input;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && input.canStillMove == false) // Reset if you've been caught
        {
            RestartGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TheLaw") // Get caught
        {
            Debug.Log("Caught!");
            endingUI.ShowUI();
            input.canStillMove = false;
            FindObjectOfType<Timer>().StopTimer();
        }
    }

    private void RestartGame()
    {
        input.ResetPlayer(); // Reset the player
        endingUI.HideUI();
        FindObjectOfType<Timer>().RestartTimer();
    }
}
