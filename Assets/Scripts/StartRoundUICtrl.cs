using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StartRoundUICtrl : MonoBehaviour
{
    public TextMeshProUGUI t;

    // Start is called before the first frame update
    void Start()
    {
        t.text = "Start Round: " +  GameManager.GetCurRoundNum();
    }

}
