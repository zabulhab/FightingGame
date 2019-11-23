﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelectScreenController : MonoBehaviour
{
    //TODO: Define all the possible moves.
    public GameObject[] masterArrayOfMoves = { };

    //Define the slots avaliable for each player
    public List<GameObject> player1slots = new List<GameObject>();
    public List<GameObject> player2Slots = new List<GameObject>();

    public int maxCapacity  = 4;

    //Capacity starts at 1 for each player
    public int player1Capacity = 0;
    public int player2Capacity = 0;

    // Negative one if game is just starting.
    // Zero if player 1. One if for player 2. Must be determined upon loading the scene. 
    public int previousWinner = -1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(previousWinner != -1)
        {
            allocateSlot();
        }
    }

    //Give player an additional slot if they won a previous match. 
    public void allocateSlot()
    {
        if (previousWinner == -1) return; 

        if(previousWinner == 1 && player1Capacity < maxCapacity)
        {
            player1Capacity++;
            player1slots[player1Capacity].SetActive(true);
        }
        if(previousWinner == 2 && player2Capacity < maxCapacity)
        {
            player2Capacity++;
            player1slots[player2Capacity].SetActive(true);
        }
    }

    public
}
