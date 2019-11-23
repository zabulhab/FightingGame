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

    public int StartingHealth;

    private int CurrentHealth;

    // Get from PlayerMovement's MostRecentMove?
    private bool IsCorrectlyBlocking;

    [SerializeField]
    private HealthBarUIUpdate healthBarUIUpdater;

    /// <summary>
    /// Hurts this player. Calls GameManager's RoundOver method
    /// </summary>
    /// <param name="Amount">Amount.</param>
    private void TakeDamage(int Amount)
    {
        if (IsCorrectlyBlocking)
        {
            CurrentHealth -= Mathf.RoundToInt(Amount * .8f);
            healthBarUIUpdater.UpdateHealthBar(CurrentHealth);
        }
        else
        {
            CurrentHealth -= Amount;
            healthBarUIUpdater.UpdateHealthBar(CurrentHealth);
        }

        if (CurrentHealth <= 0)
        {
            GameManager.RoundOver();
        }
    }
}
