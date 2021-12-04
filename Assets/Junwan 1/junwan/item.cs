using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class item : MonoBehaviourPun, IPunObservable
{
    public int ItemNum = 0;
    public bool pos;
    public bool ckck;
    public bool moveck;
    public bool boomck;
    public GameObject mom;
    public GameObject Sphere;

    public GameObject p2;
    public GameObject CarCollider;
    public int ck;

    public GameObject boomEffect;
    public GameObject bananaPre;
    public GameObject ItemSpawn;
    public GameObject rocketSpawn;
    public GameObject bombPrefab;
    public GameObject rocket;
    private GameObject aimcross;

    public GameObject enemy;
    public string TagName;
    public float shortDis;
    public List<GameObject> TagObjects;
    public bool rocketCheck;
    bool cg;
    public GameObject rockete;
    private GameObject cam;

    public CarController controller;
    PhotonView pv;
    PhotonView tv;

    int num;

    public void OnTriggerEnter(Collider other) // 충돌시 일어나는 함수
    {
        /*  if (other.tag == "item")
          {
              PhotonNetwork.Destroy(other.gameObject);
          }*/

        if (tv.IsMine)
        {
            if (other.tag == "item")
            {
                RandomItem();

                //PhotonNetwork.Destroy(other.gameObject);
            }
            if (other.tag == "Bomb" || other.tag == "rocket")
            {

                tv.RPC("BB", RpcTarget.All);
                ck = 1;
                StartCoroutine("boom");
            }

            if (other.tag == "banana")
            {
             
                tv.RPC("BB", RpcTarget.All);
                ck = 2;
                StartCoroutine("boom");
            }
        }


    }
    [PunRPC]
    void BB()
    {

        /*  mom.transform.parent = null;
          cam.GetComponent<Followcam>().enabled = false;
          Sphere.transform.position = new Vector3(-100, -100, -100);
          p2.transform.position = new Vector3(-100, -100, -100);*/
        mom.transform.parent = null;
        cam.GetComponent<Followcam>().enabled = false;
        p2.transform.position = new Vector3(-100, -100, -100);
        mom.SetActive(true);
        Sphere.transform.position = new Vector3(-100, -100, -100);
        mom.transform.rotation = Quaternion.Euler(0, 0, 0);
        pos = false;
    }
    [PunRPC]
    void Bmm()
    {
        mom.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        mom.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cam.GetComponent<Followcam>().enabled = true;
        Sphere.transform.position = mom.transform.position;
        p2.transform.position = Sphere.transform.position;
        mom.transform.position = Sphere.transform.position;
        mom.transform.parent = p2.transform;
        mom.SetActive(false);


    }
    IEnumerator boom()
    {
        yield return new WaitForSeconds(2f);
        tv.RPC("Bmm", RpcTarget.All);
    }

    void Start()
    {
        pv = gameObject.GetComponentInParent<PhotonView>();
        tv = this.GetComponent<PhotonView>();
        pos = true;
        boomck = true;
        moveck = true;
        ckck = true;
        cam = GameObject.Find("Camera");
        aimcross = GameObject.Find("aim");

        num = 0;

    }

    IEnumerator BoostDelay()
    {
        controller.fwdMaxSpeed *= 1.5f;
        controller.acceleration *= 1.5f;
        yield return new WaitForSeconds(4f);
        if (controller.fwdMaxSpeed == controller.fwdMaxSpeed * 1.5 - 10)
            controller.fwdMaxSpeed -= 30f;
        else
        {
            controller.fwdMaxSpeed = 40f;
            controller.acceleration = 0.3f;

        }
    }
    private void FixedUpdate()
    {
        switch (ItemNum)
        {
            case 0:
                break;

            case 1: // 폭탄

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    ItemSpawn.transform.localPosition = new Vector3(ItemSpawn.transform.localPosition.x, 0.7f, ItemSpawn.transform.localPosition.z);
                    //GameObject bomb = Instantiate(bombPrefab);
                    //bomb.transform.position = ItemSpawn.transform.position;
                    GameObject bomb = PhotonNetwork.Instantiate("bomb", ItemSpawn.transform.position, Quaternion.identity);
                    GameManager.instance.uiList[ItemNum].gameObject.SetActive(false);
                    ItemNum = 0;
                    num = 2;
                }
                
                break;

            case 2: // 바나나
                ItemSpawn.transform.localPosition = new Vector3(ItemSpawn.transform.localPosition.x, 0.35f, ItemSpawn.transform.localPosition.z);
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    //GameObject bananapr = Instantiate(bananaPre);
                    //bananapr.transform.position = ItemSpawn.transform.position;
                    GameObject bananapr = PhotonNetwork.Instantiate("Banana", ItemSpawn.transform.position, Quaternion.identity);
                    GameManager.instance.uiList[ItemNum].gameObject.SetActive(false);
                    ItemNum = 0;
                    num = 4;
                }
               

                break;

            case 3: //부스터

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    GameManager.instance.uiList[ItemNum].gameObject.SetActive(false);
                    ItemNum = 0;
                    StartCoroutine("BoostDelay");
                }
                                break;


            case 4: //로켓

                TagObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));
                shortDis = Vector3.Distance(gameObject.transform.position, TagObjects[0].transform.position);

                enemy = TagObjects[0];



                foreach (GameObject found in TagObjects)
                {
                    float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                    if (Distance < shortDis)
                    {
                        enemy = found;
                        shortDis = Distance;


                    }
                }

                rocketCheck = false;

                if (Input.GetKey(KeyCode.LeftControl))
                {

                    aimcross.transform.position = enemy.transform.position;
                    aimcross.transform.rotation = enemy.transform.rotation;
                }

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {

                    StartCoroutine(dealeay());
                }
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {

                    if (aimcross.GetComponent<SpriteRenderer>().color == Color.red) //&& !enemy.name.Equals(this.name))
                    {
                        cg = false;
                        if (cg == false)
                        {
                            // rocket.transform.position = ItemSpawn.transform.position;
                            // Instantiate(rocket, rockete.transform);

                            //GameObject temp = Instantiate(rocket);
                            //temp.transform.position = rocketSpawn.transform.position;

                            // rocket.transform.parent = rockete.transform;
                            GameObject rocket = PhotonNetwork.Instantiate("rocket", rocketSpawn.transform.position, Quaternion.identity);
                            cg = true;
                            aimcross.transform.position = new Vector3(-100, -100, -100);
                            GameManager.instance.uiList[ItemNum].gameObject.SetActive(false);
                            ItemNum = 0;
                        }
                    }
                    else
                    {
                        aimcross.transform.position = new Vector3(-100, -100, -100);
                        GameManager.instance.uiList[ItemNum].gameObject.SetActive(false);
                        ItemNum = 0;
                    }
                }


                IEnumerator dealeay()
                {
                    while (Input.GetKey(KeyCode.LeftControl))
                    {
                        aimcross.GetComponent<SpriteRenderer>().color = Color.red;
                        aimcross.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        yield return new WaitForSecondsRealtime(0.5f);

                        aimcross.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                        aimcross.GetComponent<SpriteRenderer>().color = Color.blue;
                        yield return new WaitForSecondsRealtime(0.5f);
                    }
                }

                break;

            default:
                break;
        }
    }
    void RandomItem()
    {
        if (num == 0)
        {
            ItemNum = 1;
        }// Random.Range(1, 5); // 0이 되면은 아이템이 없는것

        else{
            ItemNum = num;
        }
        
        GameManager.instance.SetItemUi(ItemNum);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
