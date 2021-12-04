using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager net = null;
    
    public Text TxtPlayerWelcome;
    public InputField IFRoomName;
    //private GameManager gm;
    public Text TxtRoomName;
    public Button[] BtnRooms;
    public Text[] Player;
    public Text TxtNetState;
    public ChatManager chat;
    public Text[] rank;
    private string gameVersion = "1";
    List<RoomInfo> roomList = new List<RoomInfo>(); //포톤 내의 RoomInfo 스크립트 존재
    public static string playerName;
    //public static bool isEnd = false;
    public static bool isNickName = false;
    public int themeN;
    public int themeplease;
    PhotonView pv;   

    //1. 서버----------------------------------
    private void Awake()
    {
        if (net != null) Destroy(this);
        else net = this;

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        Screen.SetResolution(800, 600, false); //해상도
        if (SceneManager.GetActiveScene().name.Equals("Start_Scene"))
        {
            chat = GameObject.Find("ChatManager").GetComponent<ChatManager>();
            pv = GetComponent<PhotonView>();
        }               
    }

    public void Connect()
    {
        TxtNetState.text = PhotonNetwork.NetworkClientState.ToString();
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect");
        ConnectLobby();
    }

    public void ConnectLobby()
    {
        if (isNickName == false)
        {
            PhotonNetwork.LocalPlayer.NickName = GameManager.instance.IFNick.text;
        }
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        if(GameManager.instance.PnNick.activeSelf == true)
        {
            GameManager.instance.Confirm_btn();
        }
        TxtPlayerWelcome.text = playerName+"님 환영합니다!";
        roomList.Clear();
    }

    public void ConnectRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //여기까지 로비입장--

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        RoomInNameChange();
        GameManager.instance.RoomEnter_btn();

        pv.RPC("ImageChangeRoom", RpcTarget.All, themeplease);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }

    [PunRPC]
    private void ImageChangeRoom(int a)
    {
        if (a == 1) { Debug.Log("Kitchen_Scene"); GameManager.instance.ImageChange.sprite = GameManager.instance.serverImage[0]; }
        if (a == 2) { Debug.Log("Bedroom_Scene"); GameManager.instance.ImageChange.sprite = GameManager.instance.serverImage[1]; }
        if (a == 3) { Debug.Log("Beach_Scene"); GameManager.instance.ImageChange.sprite = GameManager.instance.serverImage[2]; }
        if (a == 4) { Debug.Log("Bathroom_Scene"); GameManager.instance.ImageChange.sprite = GameManager.instance.serverImage[3]; }
    }

    //여기까지 서버 관련----------------------------

    //2. 방만들기(PnRoomMake)-----------------------------------
    public void themenumSetting(int tn)
    {
        themeN = tn;
    }
    
    public void RoomCreateConfirm_btn() //PnRoomMake에서 확인버튼
    {       
        if (themeN != 0) //방 선택 됨
        {
            GameManager.instance.PnRoomMake.SetActive(false);
            GameManager.instance.PnLobby.SetActive(true);
            GameManager.instance.tntn = themeN;
            //DropdownValue dv = new DropdownValue();
            int maxP = 4;
            if (DropdownValue.getNum == 0) { maxP = 2; }
            else if (DropdownValue.getNum == 1) { maxP = 3; }
            else if (DropdownValue.getNum == 2) { maxP = 4; }

            PhotonNetwork.CreateRoom(IFRoomName.text, new RoomOptions { MaxPlayers = (byte)maxP });
        }        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        themeplease = themeN;
    }

    //방 리스트 갱신
    public void RoomListNameSet()
    {
        for (int i = 0; i < BtnRooms.Length; i++)
        {
            BtnRooms[i].interactable = (i < roomList.Count) ? true : false;
            BtnRooms[i].transform.GetChild(0).GetComponent<Text>().text = (i < roomList.Count) ? roomList[i].Name : "방 없음";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> reroomList)
    {
        int roomCount = reroomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!reroomList[i].RemovedFromList) //현재 존재하는 방이라면,
            {
                if (!roomList.Contains(reroomList[i])) roomList.Add(reroomList[i]);
                else roomList[roomList.IndexOf(reroomList[i])] = reroomList[i];
            }
            else if (roomList.IndexOf(reroomList[i]) != -1)
            {
                roomList.RemoveAt(roomList.IndexOf(reroomList[i]));
            }
        }
        RoomListNameSet();
    }
    //여기까지 방 만들기---------------------------------------------

    //3. 방입장(PnRoom)-----------------------------------------------------
    public void RoomClick_btn(int roomNum) //PnLobby에서 방 클릭
    {
        PhotonNetwork.JoinRoom(roomList[roomNum].Name);
        RoomListNameSet();//리스트에 방 이름 넣는 함수, 로비에 방 이름 보이게 설정
    }

    public void RoomInNameChange() //PnRoom 방에 들어갔을 때, 방 이름, 플레이어 이름 변경
    {
        TxtRoomName.text = PhotonNetwork.CurrentRoom.Name;

        for (int i = 0; i < Player.Length; i++)
        {
            Player[i].text = "";
        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {

            Player[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    public void RoomLeft()
    {
        Debug.Log("RoomLeft");
        PhotonNetwork.LeaveRoom();
    }

    public void EndGameRoomLeft()
    {
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
        GameManager.instance.camSet = false;
        PhotonNetwork.LeaveRoom();
    }


    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
        SceneManager.LoadScene("Start_Scene");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        RoomInNameChange();
        if (SceneManager.GetActiveScene().name == "Start_Scene")
        {
            chat.Chatting("<color=red>" + newPlayer.NickName + "님이 참가하였습니다</color>");
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
        RoomInNameChange();
        if (SceneManager.GetActiveScene().name == "Start_Scene")
        {
            chat.Chatting("<color=red>" + otherPlayer.NickName + "님이 퇴장하였습니다</color>");
        }
    }

    //여기까지 방입장------------------------------------------------

    void Start()
    {
        if (isNickName == true && SceneManager.GetActiveScene().name == "Start_Scene")
        {
            GameManager.instance.EndGameLobby();
        }
    }

    void Update()
    {
        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            //rank[i].text = PhotonNetwork.PlayerList[i].NickName;
    }
}
