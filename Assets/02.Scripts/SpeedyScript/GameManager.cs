using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static CarController carControl;
    public bool checkpoint;

    [Header("Menu")]
    public GameObject startMenu;
    //public GameObject selectMenu; //Ready UI

    //[서버 관련]
    public InputField IFNick;

    public GameObject PnNick;
    public GameObject PnLobby;
    public GameObject PnRoomMake;
    public GameObject PnRoom;
    public GameObject player;
    public Boolean camSet = false;
    public GameObject[] playerObj;
    public Sprite[] serverImage;
    public Image ImageChange;
    public GameObject StartPosition;
    //[서버 관련 끝]

    public GameObject ui;
    //public TextMeshProUGUI countText;
    public TextMeshProUGUI countText;
    public GameObject menuSet;
    public GameObject title;
    public GameObject sett;
    public GameObject itemins;
    public GameObject itemdes;
    //public TextMeshProUGUI curTimeText;
    public Text curTimeText;
    public GameObject fianlMenu;
    public GameObject check;
    public GameObject round;

    PhotonView pv;
    float curTime;

    // --------------- 차량관련 
    public float baseSpeed = 5f; // 기본속도

    [Header("GameObj")]
    public Car[] car;
    public Transform[] target;

    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        
    }
    public void EndGameLobby()
    {
        startMenu.SetActive(false);
        PnLobby.SetActive(true);
    }
    void SpeedSet() // 랜덤속도 부여스크립트
    {
        for (int i = 0; i < car.Length; i++)
        {
            car[i].carSpeed = UnityEngine.Random.Range(baseSpeed, baseSpeed + 0.5f);
        }
    }
    void stopCars()
    {
        for (int i = 0; i < car.Length; i++)
        {
            car[i].goFlag = false;
        }
    }
    void startCars()
    {
        for (int i = 0; i < car.Length; i++)
        {
            car[i].goFlag = true;
        }
    }
    // ---------------------------------- 차량 관련
    public void GameStart()
    {
        Debug.Log("GameStart");
        //StartPosition.transform.position
        //Vector3 pos = new Vector3(UnityEngine.Random.Range(-14, -13), 4.252f, -17f);
        /*UnityEngine.Random rand = new UnityEngine.Random();
        rand.NextDouble();*/
        Vector3 pos = StartPosition.transform.position;
        //Vector3 pos2 = new Vector3()
        Quaternion rot = StartPosition.transform.rotation;
        player = PhotonNetwork.Instantiate(playerObj[UnityEngine.Random.Range(0, playerObj.Length)].name, pos, rot);
        player.name = NetworkManager.playerName;
        
        //player2.name = RankCopy.instance.rank2.text;
        //player3.name = RankCopy.instance.rank3.text;
        //player4.name = RankCopy.instance.rank4.text;
        //player = PhotonNetwork.Instantiate("PlayerFix", pos, Quaternion.identity);
        
        camSet = true;
        carControl = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();

        if (instance == null)
            instance = this;
        SpeedSet();

        stopCars();
        carControl.goFlag = false;
        StartCoroutine("StartCount");

    }

    IEnumerator StartCount()   //3,2,1,go!
    {
        //selectMenu.SetActive(false);
        ui.SetActive(true); menuSet.SetActive(false);

        SE_Manager.instance.PlaySound(SE_Manager.instance.count);
        countText.text = "3";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);

        countText.text = "2";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);

        countText.text = "1";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);

        countText.text = "GO!";
        startCars();
        carControl.goFlag = true;
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);

        StartCoroutine("Timer");

    }

    IEnumerator Timer()   // 타이머
    {
        while (true)
        {
            curTime += Time.deltaTime;

            curTimeText.text = string.Format("{0:00}:{1:00.00}", (int)(curTime / 60 % 60), curTime % 60);
            yield return null;
        }
    }

    public void Start_btn()
    {
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
        startMenu.SetActive(false);
        //selectMenu.SetActive(true); //-Ready 메뉴 지우기
        PnNick.SetActive(true); //--서버
        fianlMenu.SetActive(false);
    }

    /*public void Re_Start_btn()
    {
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
        //SceneManager.LoadScene(0);
        Debug.Log("Re_Start_btn");
    }*/

    public void Select_btn()
    {
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }

    public void Yes_btn()
    {
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }

    public void No_btn()
    {
        //selectMenu.SetActive(false);
        startMenu.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }

    //[서버관련]
    //닉네임 설정--------------------------------------------
    public void Confirm_btn() //PnNick에서 닉네입 입력 후 확인 버튼
    {
        PnNick.SetActive(false);
        PnLobby.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
        NetworkManager.playerName = PhotonNetwork.LocalPlayer.NickName;
        NetworkManager.isNickName = true;
    }

    public void Cancel_btn() //PnNick에서 취소 버튼
    {
        PnNick.SetActive(false);
        startMenu.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }
    //-----------------------------------------------------

    //PnLobby(로비화면)-------------------------------------
    public void RoomMake_btn() //PnLobby에서 방 만들기 버튼
    {
        PnLobby.SetActive(false);
        PnRoomMake.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }

    public void QuickStart_btn() //PnLobby에서 빠른 시작 버튼
    {
        PnLobby.SetActive(false);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }
    //----------------------------------------------------

    //PnRoomMake(방 만들기)-------------------------------
    public void RoomCreateCancel_btn() //PnRoomMake에서 취소 버튼
    {
        PnRoomMake.SetActive(false);
        PnLobby.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }
    //----------------------------------------------------

    //PnRoom(룸 화면)--------------------------------------
    public void RoomEnter_btn() //방 입장
    {
        PnRoom.SetActive(true);
    } 
    //게임시작---------------------------------------------
    public int tntn;

    public void GameStart_btn()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            //Debug.Log("Master만 게임 시작을 할 수 있습니다.");
            return;
        }
        
        if (tntn == 1)
        {
            PhotonNetwork.LoadLevel("Kitchen_Scene");
        }
        else if (tntn == 2)
        {
            PhotonNetwork.LoadLevel("Bedroom_Scene");
        }
        else if (tntn == 3)
        {
            PhotonNetwork.LoadLevel("Beach_Scene");
        }
        else if (tntn == 4)
        {
            PhotonNetwork.LoadLevel("Bathroom_Scene");
        }
    }    

    //----------------------------------------------------
    //[서버 끝]--------------------------------------------

    //----------------------------------------------------
    public void Item_btn()  //아이템 설명-------------
    {
        PnLobby.SetActive(false);
        itemins.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }

    public void itemNext_btn()
    {
        itemins.SetActive(false);
        itemdes.SetActive(true);
    }

    public void itemPrevious_btn()
    {
        itemdes.SetActive(false);
        itemins.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }

    public void itemClose_btn()
    {
        itemdes.SetActive(false);
        itemins.SetActive(false);
        PnLobby.SetActive(true);
    }
    //-----------------------------------------------------------------
    public List<Image> uiList = new List<Image>();

    public void SetItemUi(int idx)
    {
        try
        {
            foreach (var ui in uiList)
                ui.gameObject.SetActive(false);
        }
        catch (NullReferenceException) { }
        for (int i = 1; i < uiList.Count; i++)
        {
            uiList[i].gameObject.SetActive(false);
        }
        uiList[idx].gameObject.SetActive(true);
    }

    public void Esc_btn()
    {
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }
    public void GameQuit()
    {
        //Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }    
    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
    public void OnSetting()
    {
        PnLobby.SetActive(false);
        sett.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }
    public void OutSetting()
    {
        sett.SetActive(false);
        PnLobby.SetActive(true);
        SE_Manager.instance.PlaySound(SE_Manager.instance.btn);
    }
    public void Update()
    {
        //if (Input.GetButtonDown("Cancel"))
        //{
        //if (menuSet.activeSelf)
        //menuSet.SetActive(false);
        //else if (startMenu.activeSelf) menuSet.SetActive(false);
        //else if (sett.activeSelf) menuSet.SetActive(false);
        //else if (selectMenu.activeInHierarchy) menuSet.SetActive(false);
        //else if (title.activeSelf) menuSet.SetActive(false);
        //else menuSet.SetActive(true);
        //}
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Kitchen_Scene" ||
            SceneManager.GetActiveScene().name == "Bedroom_Scene" ||
            SceneManager.GetActiveScene().name == "Beach_Scene" ||
            SceneManager.GetActiveScene().name == "Bathroom_Scene")
        {
            GameStart();
        }
        if(SceneManager.GetActiveScene().name == "Start_Scene")
        {
            pv = GetComponent<PhotonView>();
        }
    }
}
