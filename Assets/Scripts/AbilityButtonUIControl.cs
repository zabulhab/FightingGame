using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A terrible script that has UI control but also an ID for this button
/// so we can get its MoveID to store in the list
/// </summary>
public class AbilityButtonUIControl : MonoBehaviour
{
    public int MovesScriptMoveID;

    /// <summary>
    /// Grays the button BG out, disables pressing the button, grays the text
    /// </summary>
    public void DisableButton()
    {
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Button>().enabled = false;
    }

    public void EnableButton()
    {
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.GetComponent<Button>().enabled = true;
    }
}
