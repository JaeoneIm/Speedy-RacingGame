using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour 
    // 코루틴을 이용한 ai가 맵을따라서 움직이는 스크립트입니다.
    // 현 스크립트는 맵을 bake해서 구워줘야 ai가 움직임으로, 현재는 사용불가합니다.
    // 해당 스크립트는 GameManager와 동시에 작동합니다.
{
    public float carSpeed;
    public Transform target;
    int nextTarget;
    public bool player;
    public bool goFlag;

    private void Start()
    {
        if (!player) // 플레이어가 아닐경우 아래의 AI함수를 실행함
        {
            target = GameManager.instance.target[nextTarget];
            GetComponent<NavMeshAgent>().speed = carSpeed;
        }
    }
    private void Update()
    {
        if (goFlag)
        {
            StartCoroutine("AI_Move"); // 코루틴은 따로 실행해줘야됨
            StartCoroutine("AI_Animation");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    IEnumerator AI_Move() // AI가 Bake된 맵을 따라서 자동으로 주행하게하는 코루틴
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        while (true)
        {
            float dis = (target.position - transform.position).magnitude; //목적지로 이동하는동안 목적지의 거리를 계산함

            if(dis <= 1) //만약 거리가 1보다 작아진다면 1을 더하여 다음 목적지로 변경을함
            {
                nextTarget += 1;
                if (nextTarget >= GameManager.instance.target.Length) // 만약 다음목적지가 그이상 넘어간다면 다시 처음목적지로
                    nextTarget = 0;

                target = GameManager.instance.target[nextTarget];
                GetComponent<NavMeshAgent>().SetDestination(target.position);
            }
            yield return null;
        }
    }
    IEnumerator AI_Animation()
    {
        Vector3 lastPosition;
        while (true)
        {
            lastPosition = transform.position;
            yield return new WaitForSecondsRealtime(0.03f);

            if ((lastPosition - transform.position).magnitude > 0) // 카트가 이동한거리가 0보다 클경우
            {
                Vector3 dir = transform.InverseTransformPoint(lastPosition);
                //if (dir.x >= -0.01f && dir.x <= 0.01f) // 직진을했을경우
                   // GetComponent<Animator>().Play("Run");
                // if (dir.x < -0.01f) // 우회전시
                // GetComponent<Animator>().Play("GoRight");
                // if (dir.x > 0.01f) // 좌회전시
                // GetComponent<Animator>().Play("GoLeft");
            }
            if((lastPosition - transform.position).magnitude <=0) // 카트가 이동한거리가 0보다 작거나 같으면 Idle
                GetComponent<Animator>().Play("Idle");

            yield return null;
        }
    }
}
