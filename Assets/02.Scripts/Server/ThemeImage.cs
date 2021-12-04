using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ThemeImage : MonoBehaviour
{
    public int receiveThemeN;
    PhotonView pv;

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(NetworkManager.net.themeN);
        }
        else
        {
            receiveThemeN = (int)stream.ReceiveNext();
        }
    }*/

    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log("masterTheme  : " + MasterTheme);
    }
}
