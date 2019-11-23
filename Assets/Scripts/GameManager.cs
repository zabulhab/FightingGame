using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

// Power ups happen between rounds, best 3/5
// TODO: Add timer: 2 mins?

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Whether the game is currently paused
    /// </summary>
    public static bool gamePaused;

    public UnityEvent OnPause;
    public UnityEvent OnUnpause;
    public UnityEvent OnRoundOver; // 1 "match"/"round"
    public UnityEvent OnBoutOver; // when at least 3/5 matches have been won by someone

    // Counts for how many times this bout each player has won
    private int P1WinCount = 0;
    private int P2WinCount = 0;

    // If 1, P1 lost most recently, if 2, P2 lost most recently
    private int MostRecentLoser = 0;

    // Which round are we on? Starts at 1 by default
    private int currentRoundNum =1;

    /// <summary>
    /// All usable moves selectable by both players
    /// </summary>
    private List<bool> AllAllowedMoves;

    /// <summary>
    /// The moves P1 selected for this round
    /// </summary>
    private List<bool> P1CurRoundMoves;

    /// <summary>
    /// THe moves P2 selected for this round
    /// </summary>
    private List<bool> P2CurRoundMoves;

    private int MainMenuSceneIdx = 0;
    private int SelectScreenIdx = 1;
    private int FightSceneIdx = 2;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    void Awake()
    {
        // if another instance was created, destroy it
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else // this is the right instance
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Accessed from the move selection menu on initial startup.
    /// Opens the scene for the first round of the bout.
    /// </summary>
    public static void EnterFightScene()
    {
        Debug.Log("FIRST FIGHT");
        // current round num already set from start to 1
        SceneManager.LoadScene(instance.FightSceneIdx);
    }

    /// <summary>
    /// Accessed from the move selection menu on initial startup.
    /// Opens the scene for the first round of the bout.
    /// </summary>
    public static void EnterMoveSelectScreen()
    {
        Debug.Log("FIRST FIGHT");
        SceneManager.LoadScene(instance.SelectScreenIdx);
    }

    /// <summary>
    /// Returns to selection menu between rounds
    /// </summary>
    public static void ProgressToSelectionMenu()
    {
        UnpauseGame();
        Debug.Log("IN SELECTION MENU BETWEEN FIGHTS");
        SceneManager.LoadScene(instance.SelectScreenIdx);
    }

    /// <summary>
    /// Restarts the current round
    /// </summary>
    public static void RestartRound()
    {
        UnpauseGame();
        //TODO: reset player health and timer
        SceneManager.LoadScene(instance.FightSceneIdx);
        Debug.Log("RESTARTING FIGHT");
    }

    /// <summary>
    /// Returns to selection menu but also quits the whole bout
    /// </summary>
    public static void QuitBout()
    {
        Debug.Log("YOU GAVE UP ON THE BOUT");
        ResetWinStats();
        SceneManager.LoadScene(instance.SelectScreenIdx);
    }


    /// <summary>
    /// Called when a player loses a round, or when time runs out.
    /// </summary>
    public static void RoundOver(int winner)
    {
        Debug.Log("ROUND OVER");
        PauseGame();
        UpdateWinCounts(winner);
        UpdateMostRecentLoser(winner);

        // if someone won 3 times, finish bout
        if (instance.P1WinCount == 3 || instance.P2WinCount == 3)
        {
            FinishAllRounds();
            ExitVictoryCallout();
        }
        else
        {
            instance.OnRoundOver.Invoke();
            instance.currentRoundNum++;
            SceneManager.LoadScene(instance.SelectScreenIdx);
        }
    }

    /// <summary>
    /// Used within a bout between rounds to go to the next round
    /// </summary>
    public static void  NextRoundStart()
    {
        SceneManager.LoadScene(instance.FightSceneIdx);
    }

    /// <summary>
    /// Called when a round ends. Determines winner and 
    /// sets the winner of this round in the list.
    /// </summary>
    /// <returns>The int of whoever won, P1 = 1, P2 = 2.</returns>
    private static int UpdateWinCounts(int winningPlayer)
    {

        if (winningPlayer == 1)
        {
            Debug.Log("A ROUND WINNER IS 1");
            instance.P1WinCount++;
            return 1;
        }
        else
        {
            Debug.Log("A ROUND WINNER IS 2");
            instance.P2WinCount++;
            return 2;
        }
    }

    /// <summary>
    /// Updates the most recent loser.
    /// </summary>
    /// <param name="WhoWon">Who just won.</param>
    private static void UpdateMostRecentLoser(int WhoWon)
    {
        if (WhoWon == 1)
        {
            instance.MostRecentLoser = 2;
        }
        else
        {
            instance.MostRecentLoser = 1;
        }
    }

    /// <summary>
    /// Triggers when one player reaches 3 wins. 
    /// Opens the victory callout.
    /// </summary>
    private static void FinishAllRounds()
    {
        Debug.Log("FINISHED BOUT");
        if (instance.P1WinCount == 3)
        {
            Debug.Log("WINNER: Player 1");
        }
        else
        {
            Debug.Log("WINNER: Player 2");
        }
    }

    /// <summary>
    /// Handles logic to show the victory pose stuff
    /// </summary>
    private static void OpenVictoryCallout()
    {
        // TODO: Coroutine for having a "victory pose" image/screen??
        // do fancy pose stuff for winner here
    }

    /// <summary>
    /// Called when the winner presses finish button on the win callout screen
    /// </summary>
    private static void ExitVictoryCallout()
    {
        Debug.Log("EXIT VICTORYCALLOUT");
        Debug.Log(instance.P1WinCount);
        Debug.Log(instance.P2WinCount);
        Debug.Log(instance.MostRecentLoser);

        ResetWinStats();

        SceneManager.LoadScene(instance.SelectScreenIdx);
    }

    /// <summary>
    /// Called when leaving a bout, either through quitting or finishing all rounds
    /// </summary>
    private static void ResetWinStats()
    {
        instance.MostRecentLoser = 0;
        instance.P1WinCount = 0;
        instance.P2WinCount = 0;
        instance.currentRoundNum = 1;
    }

    /// <summary>
    /// Returns which round we are on. Initialized to 1. 
    /// Increases by 1 each time a round is over (but not on bout over)
    /// </summary>
    /// <returns>The current round number.</returns>
    public static int GetCurRoundNum()
    {
        return instance.currentRoundNum;
    }

    public static void PauseGame()
    {
        instance.OnPause.Invoke();
        gamePaused = true;
        Time.timeScale = 0f;
    }

    public static void UnpauseGame()
    {
        instance.OnUnpause.Invoke();
        gamePaused = false;
        Time.timeScale = 1f;
    }
}
