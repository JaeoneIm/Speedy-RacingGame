using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    public float DestroyTime = 4.0f;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
