using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDelay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WarpCollider")
        {
            GetComponentInParent<CarController>().goFlag = false;
            GetComponentInParent<CarController>().moveSpeed = 0;
            StartCoroutine("WarpDelayTime");
            GetComponentInParent<CarController>().goFlag = true;
        }
    }

    IEnumerator WarpDelayTime()
    {
        yield return new WaitForSeconds(2f);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
