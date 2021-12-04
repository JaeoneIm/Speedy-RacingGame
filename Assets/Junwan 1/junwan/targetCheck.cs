using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetCheck : MonoBehaviour
{
    public GameObject p1;
    // public Rigidbody myrigid;
    Rigidbody myrigid;

    public Rigidbody sphereRb;
    public bool ck;
    bool dragck;
    void Start()
    {
        ck = true;
        dragck = true;
        myrigid = this.gameObject.transform.parent.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rocket")
        {
            ck = false;
            StartCoroutine("boom");
        }
    }


    private void Update()
    {
        if (dragck == false)
        {
            myrigid.angularDrag = 0;
            dragck = true;
        }

        if (ck == false)
        {
            p1.GetComponent<CarController>().enabled = false;
            myrigid.AddTorque(new Vector3(1, 0, 0));
        }
    }

    IEnumerator boom()
    {
        yield return new WaitForSeconds(1.3f);
        ck = true;
        myrigid.velocity = Vector3.zero;
        myrigid.angularDrag = 100;
        p1.GetComponent<CarController>().enabled = true;
        myrigid.transform.rotation = Quaternion.identity;
        sphereRb.transform.position = p1.transform.position;
        dragck = false;
        //   GameManager.carControl.goFlag = true;
        //  pos = true;

    }
    void action()
    {

        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Followcam>().enabled = true;
    }
}