using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class bananads : MonoBehaviour
{
    PhotonView pv;
    public GameObject banana;
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

            //PhotonNetwork.Destroy(banana);
            //Destroy(banana);
        }
    }
}
