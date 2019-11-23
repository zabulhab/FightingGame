using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIUpdate : MonoBehaviour
{
    private Slider HealthBar;

    public void UpdateHealthBar(int newHealth)
    {
        HealthBar.value = newHealth;
    }
}
