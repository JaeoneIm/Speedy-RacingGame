using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    public Vector3 CenterOfMassed;
    public bool Awake;
    public Rigidbody sphereRb;

    void Start()
    {
        
    }

    void Update()
    {
        sphereRb.centerOfMass = CenterOfMassed;
        sphereRb.WakeUp();
        Awake = !sphereRb.IsSleeping();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * CenterOfMassed, 0.01f);
    }
}
