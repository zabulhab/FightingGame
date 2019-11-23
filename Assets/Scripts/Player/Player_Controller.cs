using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    /* INSPECTOR */
    
    // Turn on to print debug msgs
    public bool DEBUG = false;
    [Space(10)]

    public float move_speed = 20f;
    public float combo_wait_time_sec = 0.15f;

    /* PRIVATE */

    private Rigidbody self_rbody;
    private string combo_string = "";
    private int move = 0;
    private float last_key_pressed_time = 0;

    /* USER FUNCTIONS */

    private void ComboKeyPressed()
    {
        last_key_pressed_time = Time.time;
    }

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
            ComboKeyPressed();
            combo_string += ">";
        }
        else if (Input.GetButtonDown("Back"))
        {
            ComboKeyPressed();
            combo_string += "<";
        }
        else if (Input.GetButtonDown("Jump"))
        {
            ComboKeyPressed();
            combo_string += "^";
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            ComboKeyPressed();
            combo_string += "v";
        }
        else if (Input.GetButtonDown("Block"))
        {
            ComboKeyPressed();
            combo_string += "b";
        }
        else if (Input.GetButtonDown("Punch"))
        {
            ComboKeyPressed();
            combo_string += "p";
        }
        else if (Input.GetButtonDown("Kick"))
        {
            ComboKeyPressed();
            combo_string += "k";
        }
    }

    private void Movement()
    {
        self_rbody.AddForce(new Vector3(move_speed * move * Time.fixedDeltaTime * 100, 0));
    }

    // Returns if "combo_string" is a valid combo
    private bool ValidCombo()
    {
        // A combo is only valid if it exists and
        // the player has access to it

        return false;
    }

    private void ExecuteCombo()
    {
        // Check combo_string:
        // If valid combo, execute the combo
        if (ValidCombo())
        {

        }
        // If invalid combo, check the last key,
        // if the last key is a non-lateral movement key, execute it
        else
        {

        }
    }

    private void ProcessCombos()
    {
        // Get time elapsed since the last time a combo button was pressed
        float combo_elapsed_time = Time.time - last_key_pressed_time;
        
        // If the time elapsed is greater than the set amount, then execute the combo
        if (combo_elapsed_time > combo_wait_time_sec && combo_string != "")
        {
            if (DEBUG)
            {
                Debug.Log("Combo");
                Debug.Log(combo_string);
            }
            ExecuteCombo();
            combo_string = ""; // Reset the combo string
        }
    }

    /* UNITY FUNCTIONS */

    private void Awake()
    {
        self_rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get inputs
        PollInput();

        ProcessCombos();
    }

    private void FixedUpdate()
    {
        // Move character, in fixed update to smooth movement
        Movement();
    }
}
