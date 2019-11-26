using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
    }
}
