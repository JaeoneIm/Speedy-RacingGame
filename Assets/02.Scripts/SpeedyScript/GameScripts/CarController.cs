using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CarController : MonoBehaviour, IPunObservable
{
    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;

    public float airDrag;
    private float groundDrag;

    public float fwdMaxSpeed;
    public float revMaxSpeed;
    private float fwdSpeed;
    private float revSpeed;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float acceleration;
    public float turnSpeed;
    public float alignToGroundTime;
    public LayerMask groundLayer;
    public Rigidbody sphereRb;
    private bool maxSpeedDown;

    //[서버]
    PhotonView pv;
    Vector3 curPos;
    Quaternion curRot;
    string friendNickname;
    //[서버]

    public bool goFlag = false;     // 게임매니저 안에서 카운트가 GO로 바뀔시 ture로 작동  38번째줄
    void Start()
    {
        fwdSpeed = 0;
        revSpeed = 0;
        moveSpeed = 0;
        acceleration = 0.3f;
        sphereRb.transform.parent = null;
        maxSpeedDown = false;

        groundDrag = sphereRb.drag;
        pv = GetComponent<PhotonView>(); //-서버
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (goFlag)
                GetInput();
        }
        else
        {
            sphereRb.transform.position = Vector3.Lerp(sphereRb.transform.position, curPos, Time.deltaTime * 10.0f);
            float t = Mathf.Clamp(Time.deltaTime * 10, 0f, 0.99f);
            transform.rotation = Quaternion.Lerp(transform.rotation, curRot, t);
            gameObject.name = friendNickname;

        }
    }

    private void GetInput()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        if (moveInput == 1) // 전진가속도
        {
            fwdSpeed += moveInput * Time.deltaTime * acceleration;

            moveSpeed += fwdSpeed;

            if (moveSpeed > fwdMaxSpeed)
                moveSpeed = fwdMaxSpeed; 
        }
        
        if (moveInput == -1) // 후진가속도
        {
            revSpeed += -moveInput * Time.deltaTime * acceleration;

            moveSpeed += revSpeed;

            if (moveSpeed > revMaxSpeed)
                moveSpeed = revMaxSpeed;
        }

        if (moveInput == 0) // 버튼을 누르지않을 때 속도감소
        {
            moveSpeed = moveSpeed > 0 ? moveSpeed -= Time.deltaTime * 70f : moveSpeed += Time.deltaTime * 70f;
        }

        if (moveInput != 0 && turnInput != 0)
        {
            if (maxSpeedDown == false)
            {
                fwdMaxSpeed -= 10;
                maxSpeedDown = true;
            }
        }

        if (moveInput == 0 || turnInput == 0)
        {
            if (maxSpeedDown == true)
            {
                fwdMaxSpeed += 10;
                maxSpeedDown = false;
            }
            
        }

        float moveDir = 0;
        if (moveInput == 0 || moveInput == 1)
            moveDir = 1;

        else if (moveInput == -1)
            moveDir = -1;

        float newRotation = turnInput * turnSpeed * Time.deltaTime * moveDir;

        if (isCarGrounded)
            transform.Rotate(0, newRotation, 0, Space.World);

        transform.position = sphereRb.transform.position;

        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);

        sphereRb.drag = isCarGrounded ? groundDrag : airDrag;
    }
    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            sphereRb.AddForce(transform.forward * moveSpeed * moveInput, ForceMode.Acceleration);
        }
        else
        {
            sphereRb.AddForce(transform.up * -40f);
        }
    }

    //서버--------------------------------------
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(sphereRb.transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(NetworkManager.playerName);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            friendNickname = (string)stream.ReceiveNext();
        }
    }
    //서버--------------------------------------

}
