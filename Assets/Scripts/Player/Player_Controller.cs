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

    // Allows for combo behavior functions to be easily tied in with the other fields of the combo such as ID and combo strings
    private delegate void Combo_Delegate();

    private class Combo
    {
        public Combo_Delegate combo_behavior;
        public string combo_str;
        public int combo_id;

        public Combo(Combo_Delegate cb, string cs, int ci)
        {
            combo_behavior = cb;
            combo_str = cs;
            combo_id = ci;
        }
    }

    private Rigidbody self_rbody;
    private List<Combo> valid_combos = new List<Combo>(); 
    private string combo_string = "";
    private int move = 0;
    private float last_key_pressed_time = 0;

    /* USER FUNCTIONS */
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Movement()
    {
        self_rbody.AddForce(new Vector3(move_speed * move * Time.fixedDeltaTime * 100, 0));
    }

    private void MoveJump()
    {
        Debug.Log("Jump");
    }

    private void MoveCrouch()
    {
        Debug.Log("Crouch");
    }

    private void MoveBlock()
    {
        Debug.Log("Block");
    }

    private void MovePunch()
    {
        Debug.Log("Punch");
    }

    private void MoveKick()
    {
        Debug.Log("Kick");
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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


    // Returns if "combo_string" is a valid combo
    private Combo ValidCombo()
    {
        // A combo is only valid if it exists in "valid_combos" list
        return valid_combos.Find(x => x.combo_str == combo_string);
    }

    private void ExecuteCombo()
    {
        // Check combo_string:
        // If valid combo, execute the combo
        Combo valid = ValidCombo();
        if (valid != null)
        {
            valid.combo_behavior();
        }
        // If invalid combo, check the last key,
        // if the last key is a non-lateral movement key, execute it
        else
        {
            char last_key = combo_string[combo_string.Length - 1];

            switch (last_key)
            {
                case '^':
                    MoveJump();
                    break;
                case 'v':
                    MoveCrouch();
                    break;
                case 'b':
                    MoveBlock();
                    break;
                case 'p':
                    MovePunch();
                    break;
                case 'k':
                    MoveKick();
                    break;
            }
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

    private void Start()
    {
        // Get valid combos from GameManager and store into local list
        // TODO

        /* Combos */
        valid_combos.Add(new Combo(Combos.MoveOne, "<><", 0));
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
