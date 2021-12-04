using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public GameManager gm;

        public Vector3 offset = new Vector3(0f, 7.5f, 0f);


        private void LateUpdate()
        {
            if (gm.camSet == true)
            {
                target = gm.player.transform;
                transform.position = target.position + offset;
            }

        }

        public void Start()
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }
}
