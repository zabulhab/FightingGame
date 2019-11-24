using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinHUDUI : MonoBehaviour
{
    public GameObject winGameObject;

    // Start is called before the first frame update
    public void DisplayWinScreen()
    {
        //StartCoroutine(Win());

    }

    private IEnumerator Win()
    {
        winGameObject.SetActive(true);
        winGameObject.GetComponent<TextMeshProUGUI>().text = "WINNER: P" + GameManager.GetLastWinner();
        yield return new WaitForSeconds(2);
    }

}
