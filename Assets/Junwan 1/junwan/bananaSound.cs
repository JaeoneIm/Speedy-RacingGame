using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaSound : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other) // 충돌시 일어나는 함수
    {
        if (other.tag == "Player")
        {
            this.audioSource.Play();
        }
    
    }
        void Update()
    {
        
    }
}
