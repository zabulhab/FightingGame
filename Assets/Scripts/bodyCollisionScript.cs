using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCollisionScript : MonoBehaviour
{
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
            Debug.Log("Last player move: " + lastPlayerMove);
            Debug.Log("Last Enemy move: " + lastEnemyMove);
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
