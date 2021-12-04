using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Esc : MonoBehaviour
{
    public GameObject title;
    public void OnEnable()
    {
        if (title.activeSelf)
            Time.timeScale = 1;
        else Time.timeScale = 0;
    }
    public void OnDisable()
    {
        Time.timeScale = 1;
    }
    
    void Update()
    {

    }
}
