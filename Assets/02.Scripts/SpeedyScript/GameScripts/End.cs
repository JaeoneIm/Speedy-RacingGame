using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class End : MonoBehaviour
{
    public bool checkpoint;
    public bool checkpoint2;
    public Rigidbody rigid;
    PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pv.IsMine)
        {
            if (other.gameObject.tag == "Finish")
            {
                if (checkpoint2 == true)
                {
                    GameManager.instance.ui.SetActive(false);
                    GameManager.instance.fianlMenu.SetActive(true);
                    SE_Manager.instance.PlaySound(SE_Manager.instance.end);
                    rigid.isKinematic = true;
                }
                if (checkpoint == true)
                {
                    GameManager.instance.check.SetActive(true);
                    GameManager.instance.round.SetActive(true);
                    SE_Manager.instance.PlaySound(SE_Manager.instance.goal);
                }
            }
            if (other.gameObject.tag == "Check")
            {
                if (checkpoint == true)
                {
                    checkpoint2 = true;
                }
                else
                {
                    checkpoint = true;
                    GameManager.instance.check.SetActive(false);

                }
            }
        }        
    }


}
