using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    /* INSPECTOR */
    
    // Turn on to print debug msgs
    public bool DEBUG = false;
    [Space(10)]

    public float move_speed = 10f;

    /* PRIVATE */

    private string combo_string;
    private int move;

    /* USER FUNCTIONS */

    private void PollInput()
    {
        // Get movement input
        if (Input.GetButton("Foward") && Input.GetButton("Back"))
        {
            move = 0;
        }
        else if (Input.GetButton("Foward"))
        {
            move = 1;
        }
        else if (Input.GetButton("Foward"))
        {
            move = -1;
        }
        else
        {
            move = 0;
        }

        // Get combo input
        if (Input.GetButtonDown("Forward"))
        {
            combo_string += ">";
        }
        else if (Input.GetButtonDown("Back"))
        {
            combo_string += "<";
        }
        else if (Input.GetButtonDown("Jump"))
        {
            combo_string += "^";
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            combo_string += "v";
        }
        else if (Input.GetButtonDown("Block"))
        {
            combo_string += "b";
        }
        else if (Input.GetButtonDown("Punch"))
        {
            combo_string += "p";
        }
        else if (Input.GetButtonDown("Kick"))
        {
            combo_string += "k";
        }
    }

    private void Movement()
    {

    }

    /* UNITY FUNCTIONS */

    // Start is called before the first frame update
    private void Start()
    {
        combo_string = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (DEBUG)
        {
            Debug.Log(combo_string);
        }

        // Get inputs
        PollInput();

        // Move character
        Movement();


    }
}
