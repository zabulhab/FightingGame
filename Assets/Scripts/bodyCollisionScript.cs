using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCollisionScript : MonoBehaviour
{
    public GameObject thisBodyPartsTransform;
    
    // Start is called before the first frame update
    void Start()
    {
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

        if (tagOfBodyPartCollidedWith == "WeakPoint" && tagOfThisPartsTransform == "AttackPoint")
        {

        }
        else if(tagOfBodyPartCollidedWith == "AttackPoint" && tagOfThisPartsTransform == "WeakPoint")
        {

        }
        
    }
}
