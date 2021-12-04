using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{

    private Transform real;
    Rigidbody myrigid;
    void Start()
    {
        myrigid = this.GetComponent<Rigidbody>();
        real = GameObject.Find(GameManager.instance.player.name).transform.GetChild(0);
    }

    void Update()
    {

        if (real.GetComponent<item>().ck == 1)
        {
            myrigid.AddTorque(Vector3.right);
        }
        if (real.GetComponent<item>().ck == 2)
        {
            myrigid.AddTorque(Vector3.up);
        }
    }

}