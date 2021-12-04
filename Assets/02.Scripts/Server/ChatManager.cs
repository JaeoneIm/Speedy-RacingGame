using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviour
{
    public InputField chatInput;
    public Text chatText;
    public ScrollRect scrollRect = null;
    PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {

    }

    public void MessageSendBtn()
    {
        pv.RPC("Chatting", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName + " : " + chatInput.text);
        chatInput.text = "";
    }

    [PunRPC]
    public void Chatting(string message)
    {
        chatText.text += message + "\n";
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
}
