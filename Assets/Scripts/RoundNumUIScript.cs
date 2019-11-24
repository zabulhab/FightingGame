using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundNumUIScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text RoundNumText;

    // Start is called before the first frame update
    void Start()
    {
        RoundNumText.SetText("ROUND " + GameManager.GetCurRoundNum());
    }
}
