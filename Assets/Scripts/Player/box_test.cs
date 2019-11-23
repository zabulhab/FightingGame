using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("-");
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Hit");
        }
    }
}
