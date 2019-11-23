using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; 

public class AbilitySelectScreenController : MonoBehaviour
{
    public TextMeshProUGUI selectionHeader;
    //TODO: maintain arrays with the players abilities at all times.
    public static List<int> player1Slots = new List<int>();
    public static List<int> player2Slots = new List<int>();

    //Define the slots UI avaliable for each player
    public GameObject[] player1SlotsButton;
    public GameObject[] player2SlotsButton;

    public int maxCapacity = 4;

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

        //TODO get winner from game screen

        if(previousWinner != -1)
        {
            allocateSlot();
        }
    }

    //On update, update the header depending on who should be selecting their abilities now. 
    private void Update()
    {
        if (previousWinner == -1) {
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
        else if(previousWinner == 0)
        {
            selectionHeader.text = "Player 1: Select Your Abilities!";
            selectionHeader.color = Color.red;
        }
        else if(previousWinner == 1)
        {
            selectionHeader.text = "Player 2: Select Your Abilities!";
            selectionHeader.color = Color.blue;
        }

    }
    //Give player an additional slot if they won a previous match. 
    public void allocateSlot()
    {
        if (previousWinner == -1) return; 

        if(previousWinner == 0 && player1Capacity < maxCapacity)
        {
            player1Confirmed = false;
            player1Capacity++;
            player1SlotsButton[player1Capacity].SetActive(true);
        }
        if(previousWinner == 1 && player2Capacity < maxCapacity)
        {
            player2Confirmed = false;
            player2Capacity++;
            player2SlotsButton[player2Capacity].SetActive(true);
        }
    }

    public void confirmPlayerSelection()
    {
        TextMeshProUGUI[] currentButton = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TextMeshProUGUI>();

        if (currentButton[0].name == "P1Confirm" && !player1Confirmed)
        {
            TextMeshProUGUI[] player1Move = player1SlotsButton[player1Capacity].GetComponentsInChildren<TextMeshProUGUI>();
            player1Slots.Add(int.Parse(player1Move[0].text));
            player1Confirmed = true; 
        }

        if (currentButton[0].name == "P2Confirm" && !player2Confirmed)
        {
            TextMeshProUGUI[] player2Move = player2SlotsButton[player2Capacity].GetComponentsInChildren<TextMeshProUGUI>();
            player2Slots.Add(int.Parse(player2Move[0].text));
            player2Confirmed = true;
        }

        Debug.Log(player1Confirmed);
        Debug.Log(player2Confirmed);
    }

    public void fillSlot()
    {
        if (player1Confirmed && player2Confirmed) return;

        TextMeshProUGUI[] currentButton = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TextMeshProUGUI>();

        if (previousWinner == -1)
        {
            TextMeshProUGUI[] prevWinnerText = !player1Confirmed ? player1SlotsButton[player1Capacity].GetComponentsInChildren<TextMeshProUGUI>() :
                                                                   player2SlotsButton[player2Capacity].GetComponentsInChildren<TextMeshProUGUI>();
            prevWinnerText[0].text = currentButton[0].text;
        }
        else
        {
            TextMeshProUGUI[] prevWinnerText = previousWinner == 0 ? player1SlotsButton[player1Capacity].GetComponentsInChildren<TextMeshProUGUI>() :
                                                                     player2SlotsButton[player2Capacity].GetComponentsInChildren<TextMeshProUGUI>();
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
            //start the game scenen
            Debug.Log("TODO: ADD function to switch to combat scene");
        }
    }
}
