using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public int master;
    public Vector3 spawnPoint;
    private int speedMult;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = spawnPoint;
        if (master == 1)
        {
            speedMult = 1;
            this.tag = "P1_AP";
            this.GetComponent<bodyCollisionScript>().enemyTagPart = "P2_WP";
        }
        else
        {
            speedMult = -1;
            this.tag = "P2_AP";
            this.GetComponent<bodyCollisionScript>().enemyTagPart = "P1_WP";
        }
        this.GetComponent<Rigidbody>().velocity = new Vector3(5f * speedMult, 0f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}
