﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCollisionScript : MonoBehaviour
{
    public GameObject thisBodyPartsTransform;
    private Player_Controller playerScript; 

    // Start is called before the first frame update
    void Awake()
    {
        playerScript = thisBodyPartsTransform.GetComponent<Player_Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If the opposing player hits the current players body part

    private void OnCollisionEnter(Collision collision)
    {
        string tagOfBodyPartCollidedWith = collision.transform.tag;
        string tagOfThisPartsTransform = thisBodyPartsTransform.tag;
        Player_Controller enemyPlayerController = collision.gameObject.GetComponent<Player_Controller>();
        //Debug.Log(enemyPlayerController);

        //Debug.Log(playerScript);
        //int lastPlayerMove = playerScript.GetRecentMove();
       
        if (tagOfBodyPartCollidedWith == "WeakPoint" && tagOfThisPartsTransform == "AttackPoint")
        {
            Debug.Log(enemyPlayerController);

        }
        else if(tagOfBodyPartCollidedWith == "AttackPoint" && tagOfThisPartsTransform == "WeakPoint")
        {

        }
    }
}
