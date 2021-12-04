using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public Transform tr0; // 플레이어 본인
    public Transform tr1; // bot1
    public Transform tr2; // bot2
    public Transform tr3; // bot3

    public List<int> rNum = new List<int>(); // 플레이어 크기만큼 생성한 리스트
    public List<int> alreadyList = new List<int>(); // 
    public List<Vector3> vectorList = new List<Vector3>();

    public int ItemNum = 0; // 아이템코드값

    public GameObject bombPrefab;   // 폭탄프리팹
    public GameObject bombPosition; // 폭탄스폰위치
    public static CarControl carControl;
    public void OnTriggerEnter(Collider other) // 충돌시 일어나는 함수
    {
        if (other.tag == "Item")
        {
            RandomItem();
        }
        if (other.tag == "Bomb") // 폭탄과 부딪혔을경우
        {
            tr0.Translate(new Vector3(0, 2, 0));
        }
        if (other.tag == "Finish") { }
        else if (other.tag == "Check") { }
        else Destroy(other.gameObject);
    }

    void RandomItem()
    {
        ItemNum = UnityEngine.Random.Range(1, 3); // 0이 되면은 아이템이 없는것
        GameManager.instance.SetItemUi(ItemNum);
    }
    void shuffle()
    {
        int i, j, temp;
        int cnt = 0;
        while (cnt != 4)
        {
            cnt = 0;
            for (i = 0; i < 4; i++)
            {
                temp = UnityEngine.Random.Range(0, 4); // 0 부터 3까지 랜덤숫자를 뽑아냄
                rNum[i] = temp; // 현재 생성번호
                for (j = 0; j < i; j++) // 이전에 생성된 번호와 같은지 비교
                    if (rNum[j] == temp) // 중복이 존재하게되면
                    {
                        i--; // 중복되지 않게 다시 랜덤을 돌림
                        cnt++;
                    }
            }
        }
    }
    public void RandomPosition()
    {
        // 랜덤 위치 변경 아이템
        shuffle(); // 중복되지 않는 숫자 리스트

        vectorList.Add(tr0.position); // 0
        vectorList.Add(tr1.position); // 1
        vectorList.Add(tr2.position); // 2
        vectorList.Add(tr3.position); // 3

        // 각자 다른 위치가 나올때까지 반복
        if (tr0.position == vectorList[rNum[0]])
        {
            RandomPosition();
            return;
        }
        if (tr1.position == vectorList[rNum[1]])
        {
            RandomPosition();
            return;
        }
        if (tr2.position == vectorList[rNum[2]])
        {
            RandomPosition();
            return;
        }
        if (tr3.position == vectorList[rNum[3]])
        {
            RandomPosition();
            return;
        }

        tr0.position = vectorList[rNum[0]];
        tr1.position = vectorList[rNum[1]];
        tr2.position = vectorList[rNum[2]];
        tr3.position = vectorList[rNum[3]];

        // 위치를 변경하고 난후, 위치를변경해줄 백터리스트값 초기화
        vectorList.Clear();
    }

    private void Awake()
    {
        carControl = GameObject.FindGameObjectWithTag("Player").GetComponent<CarControl>();
        bombPosition = GameObject.Find("BombSpawner");
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            switch (ItemNum)
            {
                case 1: // 1번코드에 맞는 아이템 함수 실행 ex)부스터
                    ItemNum = 0;
                    break;

                case 2: // 2번코드에 맞는 아이템 함수 실행 ex)폭탄
                    Vector3 position = bombPosition.gameObject.transform.position;
                    GameObject bomb = Instantiate(bombPrefab);
                    bomb.transform.position = position;
                    ItemNum = 0;
                    break;

                case 3: // 랜덤 위치 변경 아이템
                    RandomPosition();
                    break;

                default:
                    break;
            }
        }
    }
}
