using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class target : MonoBehaviour
{
  /*  public GameObject aim;
    public bool check;
    public List<GameObject> TagObjects;
    private GameObject enemy;
    public string TagName;
    public float shortDis;
    public GameObject follow;
    bool cg;
    public GameObject rocket;
    public GameObject player;

    void Start()
    {
        TagObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));
        shortDis = Vector3.Distance(gameObject.transform.position, TagObjects[0].transform.position);
    }

    private void FixedUpdate()
    {
        enemy = TagObjects[0];

        foreach (GameObject found in TagObjects)
        {
            float Distance = Vector3.Distance(player.transform.position, found.transform.position);

            if (Distance < shortDis)
            {
                enemy = found;
                shortDis = Distance;
                aim = enemy;
            }
        }
    }

    void Update()
    {
        if (aim.gameObject.transform.parent.GetComponent<Rigidbody>())               //GameObject.Find("PlayerFix").GetComponent<Item>().rocketCheck == false
        {

            if (Input.GetKey(KeyCode.LeftControl))
            {

                this.transform.position = aim.transform.position;
                this.transform.rotation = aim.transform.rotation;
              
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                StartCoroutine(dealeay());
              
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {

                if (this.GetComponent<Image>().color == Color.red)
                {
                    cg = false;
                    if (cg == false)
                    {
                      
                        rocket.transform.position = aim.transform.position;
                        Instantiate(rocket);
                        cg = true;
                        this.transform.position = new Vector3(0, 0, 0);
                 
                    }

                }
                GameObject.Find("PlayerFix").GetComponent<Item>().rocketCheck = true;

            }

            IEnumerator dealeay()
            {
                while (Input.GetKey(KeyCode.LeftControl))
                {

                    this.GetComponent<Image>().color = Color.red;
                    this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    yield return new WaitForSecondsRealtime(0.5f);

                    this.transform.localScale = new Vector3(0.006f, 0.006f, 0.006f);
                    this.GetComponent<Image>().color = Color.blue;
                    yield return new WaitForSecondsRealtime(0.5f);
                }
            }

        }
    }*/
}
