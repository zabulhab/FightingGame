using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

// TODO: Add timer

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Whether the game is currently paused
    /// </summary>
    public static bool gamePaused;

    public UnityEvent OnPause;
    public UnityEvent OnUnpause;
    public UnityEvent OnGameOver;

    private int MainMenuSceneIdx = 1;
    private int SelectScreenIdx = 2;
    private int FightSceneIdx = 3;

    /// <summary>
    /// If P1 lost last round, =1; if P2, = 2; if first round, = 0
    /// </summary>
    public int LastLostPlayerNum = 0;

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
    /// Accessed from the move selection menu. Switches the scene.
    /// </summary>
    public static void EnterFightScene()
    {
        SceneManager.LoadScene("FightSceneTest");
    }

    public static void ReturnToSelectionMenu()
    {
        UnpauseGame();
        SceneManager.LoadScene("SelectScreenTest");
    }

    public static void RestartRound()
    {
        UnpauseGame();
        SceneManager.LoadScene("FightSceneTest");
    }

    public static void GameOver()
    {
        PauseGame();
        SetLosingPlayer();
        instance.OnGameOver.Invoke();
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

    /// <summary>
    /// Called on Game Over. Sets the player who lost this game. 
    /// </summary>
    private static void SetLosingPlayer()
    {
        // find player1 in scene, check if lost
        // if not lost, p2 has lost
    }

}
