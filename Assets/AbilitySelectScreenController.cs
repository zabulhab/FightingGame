using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; 

public class AbilitySelectScreenController : MonoBehaviour
{
    //TODO: Define all the possible moves.
    public GameObject[] masterArrayOfMoves = { };

    //Define the slots avaliable for each player
    public GameObject[] player1Slots;
    public GameObject[] player2Slots;

    public int maxCapacity  = 4;

    //Capacity starts at 1 for each player
    public int player1Capacity = 0;
    public int player2Capacity = 0;

    // Negative one if game is just starting.
    // Zero if player 1. One if for player 2. Must be determined upon loading the scene. 
    public int previousWinner = -1;

    bool player1Confirmed = false;
    bool player2Confirmed = false; 

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
            player1Slots[player1Capacity].SetActive(true);
        }
        if(previousWinner == 2 && player2Capacity < maxCapacity)
        {
            player2Capacity++;
            player1Slots[player2Capacity].SetActive(true);
        }
    }

    public void confirmPlayerSelection()
    {
        TextMeshProUGUI[] currentButton = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TextMeshProUGUI>();

        if (currentButton[0].name == "P1Confirm")
        {
            player1Confirmed = true; 
        }

        if (currentButton[0].name == "P2Confirm")
        {
            player2Confirmed = true;
        }
        Debug.Log(player1Confirmed);
        Debug.Log(player2Confirmed);
    }

    public void fillSlot()
    {
        TextMeshProUGUI[] currentButton = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TextMeshProUGUI>();

        if (previousWinner == -1)
        {
            TextMeshProUGUI[] prevWinnerText = !player1Confirmed ? player1Slots[player1Capacity].GetComponentsInChildren<TextMeshProUGUI>() :
                                                                   player2Slots[player2Capacity].GetComponentsInChildren<TextMeshProUGUI>();
            prevWinnerText[0].text = currentButton[0].text;
        }
        else
        {
            TextMeshProUGUI[] prevWinnerText = previousWinner == 1 ? player1Slots[player1Capacity].GetComponentsInChildren<TextMeshProUGUI>() : 
                                                                     player2Slots[player2Capacity].GetComponentsInChildren<TextMeshProUGUI>();
            prevWinnerText[0].text = currentButton[0].text;
        }
    }

    public void resetSelection()
    {
        player1Confirmed = false;
        player2Confirmed = false;
    }

    public void startGame()
    {
        if(player1Confirmed && player2Confirmed)
        {
            //TODO: Do the following:
                //pass options that the last player who won selected to the game scene.
                //start the game scenen
            Debug.Log("TODO: ADD function to switch to combat scene");
        }
    }
}
