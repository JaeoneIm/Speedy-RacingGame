using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Followcam : MonoBehaviour
{
    public Transform targetTr;
    public float dist = 2.0f;//카메라와의 거리
    public float height = 1.0f;//카메라의 높이
    public float dampTrace = 20.0f;//부드러운 추적을 위한
    private Transform tr;
    public GameManager gm;

    void Start()
    {
        tr = GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gm.camSet == true)
        {
            targetTr = gm.player.transform;
            tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
            tr.LookAt(targetTr.position);
        }
    }
}
