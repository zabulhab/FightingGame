using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    /* INSPECTOR */
    
    // Turn on to print debug msgs
    public bool DEBUG = false;
    public bool ALL_MOVES = false;
    [Space(10)]

    public string player_num = "";
    public float move_speed = 20f;
    public float jump_force = 10f;
    public float crouch_mult = 0f;
    public float combo_wait_time_sec = 0.15f;
    [Space(10)]

    public Transform ground_check;

    /* PRIVATE */

    /*
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
    */

    private Rigidbody self_rbody;
    private Animator self_animator;
    private List<int> valid_combos = new List<int>(); 
    private string combo_string = "";
    private int last_used_move = 0;
    private int move = 0;
    private float last_key_pressed_time = 0;
    private bool crouching = false;
    private bool blocking = false;
    private bool can_jump = true;

    /* USER FUNCTIONS */
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Public functions
    public int GetRecentMove()
    {
        return last_used_move;
    }

    public void SetCanJump()
    {
        can_jump = true;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// BASIC MOVES
    private void Movement()
    {
        float move_mult = 1;
        // Crouch
        if (crouching)
        {
            move_mult = crouch_mult; // Reduce movement while crouching
            self_animator.SetBool("crouch", true);
        }
        else
        {
            self_animator.SetBool("crouch", false);
        }

        // Block
        if (blocking)
        {
            move_mult = 0; // Dont move while blocking
            self_animator.SetBool("block", true);
        }
        else
        {
            self_animator.SetBool("block", false);
        }

        // Lateral
        self_rbody.AddForce(new Vector3(move_speed * move * Time.fixedDeltaTime * 100 * move_mult, 0));
        self_animator.SetFloat("move_spd", Mathf.Abs(move));
    }

    private void MoveJump()
    {
        if (can_jump)
        {
            can_jump = false;
            //Debug.Log("Jump");
            self_rbody.AddForce(new Vector3(0, jump_force));
            self_animator.SetBool("jump", true);
        }
    }

    private void MovePunch()
    {
        Debug.Log("Punch");
        self_animator.SetBool("punch", true);
    }

    private void MoveKick()
    {
        //Debug.Log("Kick");
        self_animator.SetBool("kick", true);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Combos

    private void Play_Combo(int combo_num)
    {
        //Debug.Log("Play_Combo");
        //Debug.Log(combo_num);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void ComboKeyPressed()
    {
        last_key_pressed_time = Time.time;
    }

    private void PollInput()
    {
        // Get lateral movement input
        if (Input.GetButton("Forward" + player_num) && Input.GetButton("Back" + player_num))
        {
            move = 0;
        }
        else if (Input.GetButton("Forward" + player_num))
        {
            move = 1;
        }
        else if (Input.GetButton("Back" + player_num))
        {
            move = -1;
        }
        else
        {
            move = 0;
        }

        // Get crouch
        if (Input.GetButtonDown("Crouch" + player_num))
        {
            crouching = true;
        }
        else if (Input.GetButtonUp("Crouch" + player_num))
        {
            crouching = false;
        }

        // Get block
        if (Input.GetButtonDown("Block" + player_num))
        {
            blocking = true;
        }
        else if (Input.GetButtonUp("Block" + player_num))
        {
            blocking = false;
        }

        // Get combo input
        if (Input.GetButtonDown("Forward" + player_num))
        {
            ComboKeyPressed();
            combo_string += ">";
        }
        else if (Input.GetButtonDown("Back" + player_num))
        {
            ComboKeyPressed();
            combo_string += "<";
        }
        else if (Input.GetButtonDown("Jump" + player_num))
        {
            ComboKeyPressed();
            combo_string += "^";
        }
        else if (Input.GetButtonDown("Crouch" + player_num))
        {
            ComboKeyPressed();
            combo_string += "v";
        }
        else if (Input.GetButtonDown("Block" + player_num))
        {
            ComboKeyPressed();
            combo_string += "b";
        }
        else if (Input.GetButtonDown("Punch" + player_num))
        {
            ComboKeyPressed();
            combo_string += "p";
        }
        else if (Input.GetButtonDown("Kick" + player_num))
        {
            ComboKeyPressed();
            combo_string += "k";
        }
    }

    // Returns if "combo_string" is a valid combo
    private Move ValidCombo()
    {
        // A combo is only valid if it exists in "valid_combos" list
        if (ALL_MOVES)
            return Moves.all_moves.Find(x => x.combo_string == combo_string);

        return Moves.all_moves.Find(x => x.combo_string == combo_string && valid_combos.Exists(y => y == x.move_id));
    }

    private void ExecuteCombo()
    {
        // Check combo_string:
        // If valid combo, execute the combo
        Move valid = ValidCombo();
        if (valid != null && valid.is_special)
        {
            Play_Combo(valid.move_id);
        }
        // If invalid combo, check the last key,
        // if the last key is a non-lateral movement key, execute it
        else if (valid != null)
        {
            char last_key = combo_string[combo_string.Length - 1];

            switch (last_key)
            {
                case '^':
                    MoveJump();
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
                Debug.Log("Player" + player_num);
                Debug.Log(combo_string);
            }
            ExecuteCombo();
            combo_string = ""; // Reset the combo string
        }
    }

    private void UpdateRecentMove()
    {
        // Update last_used_move based on the current state of the animator
        if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Block_High"))
            last_used_move = 15;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Block_Low"))
            last_used_move = 16;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Punch_High"))
            last_used_move = 11;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Punch_Low"))
            last_used_move = 12;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Kick_High"))
            last_used_move = 13;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Kick_Low"))
            last_used_move = 14;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_1"))
            last_used_move = 1;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_2"))
            last_used_move = 2;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_3"))
            last_used_move = 3;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_4"))
            last_used_move = 4;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_5"))
            last_used_move = 5;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_6"))
            last_used_move = 6;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_7"))
            last_used_move = 7;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_8"))
            last_used_move = 8;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_9"))
            last_used_move = 9;
        else if (self_animator.GetCurrentAnimatorStateInfo(0).IsName("Special_10"))
            last_used_move = 10;
        else
            last_used_move = 0;
    }

    /* UNITY FUNCTIONS */

    private void Awake()
    {
        self_rbody = GetComponent<Rigidbody>();
        self_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Get valid combos from GameManager and store into local list

        /* Combos */
        if (player_num == "1")
            valid_combos = GameManager.GetP1SpecialMoveInts();
        else if (player_num == "2")
            valid_combos = GameManager.GetP2SpecialMoveInts();
        else
            Debug.Log("Invalid player num");

        if (DEBUG)
        {
            foreach (int x in valid_combos)
                Debug.Log("Allowed combo index: " + x);
        }
    }

    private void Update()
    {
        // Get inputs
        PollInput();

        ProcessCombos();

        // Based on current animation
        UpdateRecentMove();
    }

    private void FixedUpdate()
    {
        // Move character, in fixed update to smooth movement
        Movement();
    }
}
