using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the health for a player throughout a round.
/// </summary>
public class Health : MonoBehaviour
{
    // Is this P1? True by default, set to false if object is P2
    // Used to identify who this script is attached to
    public bool IsPlayer1 = true;

    // TODO: make this 100 default later but keep empty for now for testing purposes
    public int StartingHealth; 

    private int CurrentHealth;

    // Get from PlayerMovement's MostRecentMove?
    private bool IsCorrectlyBlocking;

    [SerializeField]
    private HealthBarUIUpdate healthBarUIUpdater;

    private void Start()
    {
        CurrentHealth = StartingHealth;
    }

    /// <summary>
    /// Hurts this player. Calls GameManager's RoundOver method
    /// </summary>
    /// <param name="Amount">Amount.</param>
    public void TakeDamage(int Amount)
    {
        Debug.Log("OW");
        if (IsCorrectlyBlocking)
        {
            CurrentHealth -= Mathf.RoundToInt(Amount * .8f);
            healthBarUIUpdater.DepleteHealthBar(Amount);
        }
        else
        {
            CurrentHealth -= Amount;
            healthBarUIUpdater.DepleteHealthBar(Amount);
        }
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            if (IsPlayer1)
            {
                GameManager.RoundOver(2); // if p1 died, p2 wins
            }
            else
            {
                GameManager.RoundOver(1); // if p2 died, p1 wins
            }
        }
    }
}
