using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimController : MonoBehaviour
{
    public GameObject[] wheelsToRotate;
    public float rotationSpeed;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        foreach (var wheel in wheelsToRotate)
        {
            wheel.transform.Rotate(Time.deltaTime * verticalAxis * rotationSpeed, 0, 0, Space.Self);
        }

        if (horizontalAxis > 0)
        {
            anim.SetBool("goLeft", false);
            anim.SetBool("goRight", true);
        }
        else if (horizontalAxis < 0)
        {
            anim.SetBool("goRight", false);
            anim.SetBool("goLeft", true);
        }
        else
        {
            anim.SetBool("goLeft", false);
            anim.SetBool("goRight", false);
        }
    }
}
