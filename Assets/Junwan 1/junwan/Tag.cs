using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Tag : MonoBehaviour
{
    public PhotonView Player;

    private void Start()
    {

        if (Player.IsMine)
        {
            transform.GetChild(0).tag = "ppp";
        }

    }
}