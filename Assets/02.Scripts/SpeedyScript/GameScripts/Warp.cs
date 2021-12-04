using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform warpPos;
    public Transform particlePos;
    public GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.tag == "MotorSphere")
        {
            other.transform.position = warpPos.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.rotation = warpPos.rotation;
            StartCoroutine("ParticleDelayTime");
        }*/
        if(other.tag == "Carcollider" || other.tag == "ppp")
        {
            other.transform.parent.GetComponentInParent<CarController>().sphereRb.transform.position = warpPos.position;
            other.transform.parent.GetComponentInParent<CarController>().sphereRb.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.parent.transform.rotation = warpPos.rotation;
            StartCoroutine("ParticleDelayTime");
        }
        else
        {
            return;
        }       
    }

    IEnumerator ParticleDelayTime()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(particle, particlePos);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
