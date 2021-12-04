using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BombDs : MonoBehaviourPun, IPunObservable
{
    public GameObject boomEffect;
    public GameObject bomb;
    PhotonView pv;
    PhotonView tv;
    GameObject eff;
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
            eff = PhotonNetwork.Instantiate("Hit 10", bomb.transform.position, Quaternion.identity);
            StartCoroutine("efftr");
        }
    }


    IEnumerator efftr()
    {
        bomb.transform.position = new Vector3(-200, -200, -200);
        yield return new WaitForSeconds(1f);
        eff.transform.position = new Vector3(-200, -200, -200);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

}
