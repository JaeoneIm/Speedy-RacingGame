# Speedy-RacingGame
#졸업작품 #6인팀프로젝트 #멀티레이싱게임 #Unity3D

## Preview
![화면 캡처 2021-12-05 135727](https://user-images.githubusercontent.com/87745921/144734279-27d54407-0b02-4bbf-82b7-612fc0c1fc0e.png)

[시연영상(유튜브 링크)](https://youtu.be/iDd5Py1h8Ws)

## 구현한 기능 및 코드예시
**1. 캐릭터 움직임구현** (캐릭터차량 가운데 투명한 Sphere에 AddForce를 적용시켜 캐릭터의 위치를 Sphere와 동일시하는 방법)   
[코드작성시 참고한 유튜브 링크](https://youtu.be/CpXT5So1Gbg)
```C#
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

        if (moveInput != 0 && turnInput != 0) // 좌회전 및 우회전시 최고속도 감소
        {
            if (maxSpeedDown == false)
            {
                fwdMaxSpeed -= 10;
                maxSpeedDown = true;
            }
        }

        if (moveInput == 0 || turnInput == 0) // 감소된 최고속도 복구 
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
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer); // 캐릭터가 다니는 주행트랙에 groundLayer 적용되어 있음

        Quaternion toRotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);

        sphereRb.drag = isCarGrounded ? groundDrag : airDrag; // 지상에 있을 때와 공중에 있을 때의 Drag값 변경
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
```
 **2. 아이템 중 부스트 구현**         
 **3. 아이템박스와 충돌 시 랜덤한 아이템 소유 및 소유한 아이템 ui 나타나도록 구현**      
 **4. 캐릭터가 주행 도중 맵 밖이나 트랙 밖으로 떨어질 시 트랙으로 복귀시켜주는 워프 기능 및 파티클 구현**     
