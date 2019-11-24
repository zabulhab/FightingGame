using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCollisionScript : MonoBehaviour
{
    public bool DEBUG = false;
    public GameObject thisBodyPartsTransform;
    public string enemyTagPart; 
    private Player_Controller playerScript; 

    // Start is called before the first frame update
    void Awake()
    {
        playerScript = thisBodyPartsTransform.GetComponent<Player_Controller>();
    }

    //If the opposing player hits the current players body part

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("********");
        //Debug.Log(col.transform.tag);
        
        string tagOfEnemyPartCollidedWith = col.transform.tag;
        string tagOfThisPartsTransform = thisBodyPartsTransform.tag;

        Player_Controller enemyPlayerController = col.transform.root.GetComponent<Player_Controller>();

        int lastPlayerMove = playerScript.GetRecentMove();
        int lastEnemyMove = enemyPlayerController.GetRecentMove();

        if (tagOfEnemyPartCollidedWith == enemyTagPart)
        {
            //Debug.Log(enemyPlayerController);
            if (DEBUG)
            {
                Debug.Log("Last player move: " + lastPlayerMove);
                Debug.Log("Last Enemy move: " + lastEnemyMove);
            }

            // Convert plays move nums into actual moves
            Move p_move = Moves.all_moves[lastPlayerMove];
            Move e_move = Moves.all_moves[lastEnemyMove];

            if (p_move.is_attack)
            {
                // Self intended to do an attack
                if (e_move.is_block)
                {
                    // Possible attack did not go through, need to check further
                    if (p_move.target_zone == e_move.target_zone)
                    {
                        // Attack was successfully blocked, can return
                        return;
                    }
                }
                
                // Otherwise, process the attack
                // TODO: Handle damage transfer using p_move.damage

            }
        }

        /*
         if (tagOfBodyPartCollidedWith == "WeakPoint" && tagOfThisPartsTransform == "AttackPoint")
         {
             Debug.Log(enemyPlayerController);

         }
         else if(tagOfBodyPartCollidedWith == "AttackPoint" && tagOfThisPartsTransform == "WeakPoint")
         {

         }
 */
    }
}
