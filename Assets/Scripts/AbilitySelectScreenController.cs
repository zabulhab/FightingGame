﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

// TODO: Remember the existing selected moves from last round(s)
// and only enable choosing a move for the last, open slot
public class AbilitySelectScreenController : MonoBehaviour
{
    // TODO: Store a list of all abilities
    public List<GameObject> AllAbilityButtons;

    public TextMeshProUGUI selectionHeader;
    //TODO: maintain arrays with the players abilities at all times.
    public static List<int> player1Slots = new List<int>();
    public static List<int> player2Slots = new List<int>();

    //Define the slots UI avaliable for each player
    public GameObject[] player1SlotButtons;
    public GameObject[] player2SlotButtons;

    public int maxCapacity = 4; 

    //Capacity starts at 1 for each player
    public int player1CurCapacity = 1;
    public int player2CurCapacity = 1;

    // 0 if pre-round 1 (start of bout)
    // 1 for player 1. 2 for player 2. Must be determined upon loading the scene. 
    private int previousWinner = 0; // ***

    // The last move we tried to select. 
    // Used on pressing confirm to grey that button out
    private GameObject LastPressedMoveButton;

    bool player1Confirmed = false;
    bool player2Confirmed = false; 

    private void Start()
    {
        previousWinner = GameManager.GetLastWinner();

        // the capacities should be the same as the round numbers
        player1CurCapacity = GameManager.GetCurRoundNum();
        player2CurCapacity = GameManager.GetCurRoundNum();

        InitializeAbilitySlotSelectability();
        InitializePlayerSelectionSlotsForRound();
    }

    /// <summary>
    /// Initializes the selectability of each special move slot
    /// </summary>
    private void InitializeAbilitySlotSelectability()
    {
        List<int> disabledMoves = GameManager.GetAllDisabledSpecialIDs();

        if (disabledMoves == null)
        {
            Debug.Log("DISABLED MOVES EMPTY");
            return;
        }

        // get the list of ints corresponding to buttons that should be disabled
        foreach (GameObject abilityButton in AllAbilityButtons)
        {
            if (disabledMoves.Contains(abilityButton.GetComponent<AbilityButtonUIControl>().MovesScriptMoveID))
            {
                abilityButton.GetComponent<AbilityButtonUIControl>().DisableButton();
            }
        }
    }

    //On update, update the header depending on who should be selecting their abilities now. 
    private void Update()
    {
        // TODO: ADD RANDOM FIRST CHOICE
        if (previousWinner == 0) 
        {
            if (!player1Confirmed)
            {
                selectionHeader.text  = "Player 1: Select Your Abilities!";
                selectionHeader.color = Color.red;
            }
            else
            {
                selectionHeader.text = "Player 2: Select Your Abilities!";
                selectionHeader.color = Color.blue;
            }
        }
        else if(previousWinner == 1)
        {
            selectionHeader.text = "Player 2: Select Your Abilities!";
            selectionHeader.color = Color.blue;
        }
        else if(previousWinner == 2)
        {
            selectionHeader.text = "Player 1: Select Your Abilities!";
            selectionHeader.color = Color.red;
        }

    }

    /// <summary>
    /// Give both players an additional move slot on scene load.
    /// </summary>
    public void InitializePlayerSelectionSlotsForRound()
    {
        //if(previousWinner == 1 && player2Capacity < maxCapacity)
        //{
        //    player2Confirmed = false;
        //    player2Capacity++;
        //    player2SlotButtons[player2Capacity].SetActive(true);
        //}
        //if(previousWinner == 2 && player1Capacity < maxCapacity)
        //{
        //    player1Confirmed = false;
        //    player1Capacity++;
        //    player1SlotButtons[player1Capacity].SetActive(true);
        //}
        // TODO: Account for max capacity? Probably fine tho...
        for (int i = 0;  i < player1SlotButtons.Length; i++)
        {
            if (i < player1CurCapacity)
            {
                player1SlotButtons[i].SetActive(true);
            }
        }
        for (int i = 0; i < player2SlotButtons.Length; i++)
        {
            if (i < player2CurCapacity)
            {
                player2SlotButtons[i].SetActive(true);
            }
        }

        player1SlotButtons[player1CurCapacity-1].SetActive(true);
        player2SlotButtons[player2CurCapacity-1].SetActive(true);
    }

    /// <summary>
    /// Called on pressing confirm button. 
    /// Greys out and disables selecting already-selected moves.
    /// </summary>
    public void confirmPlayerSelection()
    {
        TextMeshProUGUI[] currentButton = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TextMeshProUGUI>();

        if (currentButton[0].name == "P1Confirm" && !player1Confirmed)
        {
            // if we lost 
            // OR if we won and the losing player has confirmed
            if (previousWinner != 1 || previousWinner == 1 && player2Confirmed)
            {
                TextMeshProUGUI[] player1Move = player1SlotButtons[player1CurCapacity - 1].GetComponentsInChildren<TextMeshProUGUI>();
                player1Slots.Add(int.Parse(player1Move[0].text));
                player1Confirmed = true;
                DisableLastSelectedMoveButton();

                // Add to P1 moves and remove from selectable moves
                GameManager.AddToP1SpecialMoveInts(LastPressedMoveButton.GetComponent<AbilityButtonUIControl>().MovesScriptMoveID);
                GameManager.AddToDisabledSpecialIDs(LastPressedMoveButton.GetComponent<AbilityButtonUIControl>().MovesScriptMoveID);
            }
        }

        if (currentButton[0].name == "P2Confirm" && !player2Confirmed)
        {
            // if we lost 
            // OR if we won and the losing player has confirmed
            if (previousWinner != 2 || previousWinner == 2 && player1Confirmed)
            {
                TextMeshProUGUI[] player2Move = player2SlotButtons[player2CurCapacity - 1].GetComponentsInChildren<TextMeshProUGUI>();
                player2Slots.Add(int.Parse(player2Move[0].text));
                player2Confirmed = true;
                DisableLastSelectedMoveButton();

                // Add to P2 moves and remove from selectable moves
                GameManager.AddToP2SpecialMoveInts(LastPressedMoveButton.GetComponent<AbilityButtonUIControl>().MovesScriptMoveID);
                GameManager.AddToDisabledSpecialIDs(LastPressedMoveButton.GetComponent<AbilityButtonUIControl>().MovesScriptMoveID);
            }
        }

        Debug.Log(player1Confirmed);
        Debug.Log(player2Confirmed);
    }

    /// <summary>
    /// Set the appropriate slot with the move name
    /// </summary>
    public void fillSlot(GameObject selectedMoveButton)
    {
        if (player1Confirmed && player2Confirmed) return;

        TextMeshProUGUI[] currentButton = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TextMeshProUGUI>();

        if (previousWinner == 0)
        {
            // TODO: Account for random starting player
            // if p1 hasn't confirmed, map it to p1, else map it to p2
            TextMeshProUGUI[] prevLoserOpenSlotText = !player1Confirmed ? player1SlotButtons[player1CurCapacity-1].GetComponentsInChildren<TextMeshProUGUI>() :
                                                                   player2SlotButtons[player2CurCapacity-1].GetComponentsInChildren<TextMeshProUGUI>();
            prevLoserOpenSlotText[0].text = currentButton[0].text;
        }
        else
        {
            // if p2 lost and hasn't confirmed yet, give to p2 slot
            if (previousWinner == 1 && !player2Confirmed)
            {
                TextMeshProUGUI[] prevLoserOpenSlotText = player2SlotButtons[player2CurCapacity - 1].GetComponentsInChildren<TextMeshProUGUI>();
                prevLoserOpenSlotText[0].text = currentButton[0].text;
            }
            // if p2 lost but has confirmed, give to p1 slot
            else if (previousWinner == 1 && player2Confirmed && !player1Confirmed)
            {
                TextMeshProUGUI[] prevLoserOpenSlotText = player1SlotButtons[player1CurCapacity - 1].GetComponentsInChildren<TextMeshProUGUI>();
                prevLoserOpenSlotText[0].text = currentButton[0].text;
            }
            // if p1 lost and hasn't confirmed, give to p2 slot
            else if (previousWinner == 2 && !player1Confirmed)
            {
                TextMeshProUGUI[] prevLoserOpenSlotText = player1SlotButtons[player1CurCapacity - 1].GetComponentsInChildren<TextMeshProUGUI>();
                prevLoserOpenSlotText[0].text = currentButton[0].text;
            }
            // if p1 lost but has confirmed, give to p2 slot
            else if (previousWinner == 2 && player1Confirmed && !player2Confirmed)
            {
                TextMeshProUGUI[] prevLoserOpenSlotText = player2SlotButtons[player2CurCapacity - 1].GetComponentsInChildren<TextMeshProUGUI>();
                prevLoserOpenSlotText[0].text = currentButton[0].text;
            }

        }

        // update the selected move button to be disabled
        LastPressedMoveButton = selectedMoveButton;
    }

    /// <summary>
    /// Grey out the move that was just selected
    /// </summary>
    public void DisableLastSelectedMoveButton()
    {
        LastPressedMoveButton.GetComponent<AbilityButtonUIControl>().DisableButton();
    }

    /// <summary>
    /// Un-grey out the last selected move button (probably useless?)
    /// </summary>
    public void EnableMoveButton()
    {
        LastPressedMoveButton.GetComponent<AbilityButtonUIControl>().EnableButton();
    }

    public void StartGame()
    {
        if(player1Confirmed && player2Confirmed)
        {
            GameManager.EnterFightScene();
        }
    }
}
