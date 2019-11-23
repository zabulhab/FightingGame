using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIUpdate : MonoBehaviour
{
    [SerializeField]
    private Slider HealthBar;
    [SerializeField]
    private Image HealthBarFill;

    public void DepleteHealthBar(int amount)
    {
        HealthBar.value -= amount;
        Debug.Log(HealthBar.value);
        if (HealthBar.value < 50 && HealthBar.value > 30)
        {
            HealthBarFill.GetComponent<Image>().color = Color.yellow;
        }
        else if (HealthBar.value < 30)
        {
            HealthBarFill.GetComponent<Image>().color = Color.red;
        }
        // HACK: makes leftover sliver of healthbar go away on 0
        if (HealthBar.value <= 0) 
        {
            HealthBarFill.GetComponent<Image>().enabled = false;
        }
    }
}
