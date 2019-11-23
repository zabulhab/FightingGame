using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{

    public void TestProgressFights()
    {
        Debug.Log("HI");
        GameManager.RoundOver();
    }

    public void TestStartBout()
    {
        GameManager.EnterFightScene();
    }

    public void TestEnterMoveSelect()
    {
        GameManager.EnterMoveSelectScreen();
    }

    public void QuitBout()
    {
        GameManager.QuitBout();
    }

    public void RestartRound()
    {
        GameManager.RestartRound();
    }
}
