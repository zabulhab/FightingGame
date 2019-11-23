using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    /* INSPECTOR */
    
    // Turn on to print debug msgs
    public bool DEBUG = false;
    [Space(10)]

    public float move_speed = 100f;

    /* PRIVATE */

    private Rigidbody self_rbody;
    private string combo_string = "";
    private int move = 0;

    /* USER FUNCTIONS */

    private void PollInput()
    {
        // Get movement input
        if (Input.GetButton("Forward") && Input.GetButton("Back"))
        {
            move = 0;
        }
        else if (Input.GetButton("Forward"))
        {
            move = 1;
        }
        else if (Input.GetButton("Back"))
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
        self_rbody.AddForce(new Vector3(move_speed * move, 0));
    }

    /* UNITY FUNCTIONS */

    // Start is called before the first frame update
    private void Awake()
    {
        self_rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (DEBUG)
        {
            Debug.Log("Combo string");
            Debug.Log(combo_string);

            Debug.Log("Move");
            Debug.Log(move);
        }

        // Get inputs
        PollInput();

        // Move character
        Movement();
    }
}
