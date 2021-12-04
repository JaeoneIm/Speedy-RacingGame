using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class boxds : MonoBehaviour
{
    PhotonView pv;
    void Start()
    {
        pv = GameObject.Find(GameManager.instance.player.name).GetComponent<PhotonView>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine == false)
        {
            return;
        }
        if (other.tag == "Carcollider" || other.tag == "ppp")
        {
            this.transform.position = new Vector3(-200, -200, -200);
        }
    }
}