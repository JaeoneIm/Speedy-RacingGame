using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Missile : MonoBehaviourPun, IPunObservable
{
    public GameObject boomEffect;
    public GameObject bomb;
    private GameObject player;
    private Transform playerTr;
    Rigidbody Rocketrigid;
    public float turn;
    public float Velocity;
    PhotonView pv;

    GameObject eff;

    private void Start()
    {
        Rocketrigid = this.GetComponent<Rigidbody>();
        player = GameObject.Find(GameManager.instance.player.name);

        pv = player.GetComponent<PhotonView>();
    }

    public void FixedUpdate()
    {
        playerTr = player.transform.GetChild(0).GetComponent<item>().enemy.transform;
        Rocketrigid.velocity = transform.forward * Velocity;

        Vector3 dir = playerTr.transform.position - transform.position;
        Quaternion Rotation = Quaternion.LookRotation(dir.normalized);
        transform.rotation = Rotation;

        // var Rotation = Quaternion.LookRotation(playerTr.transform.position + new Vector3(0, 0.1f) - transform.position);
        // Rocketrigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, Rotation, turn));
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





