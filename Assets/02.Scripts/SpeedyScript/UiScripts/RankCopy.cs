using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RankCopy : MonoBehaviour
{
    public static RankCopy instance;
    public Text text1;
    public Text copy1;
    public Text text2;
    public Text copy2;
    public Text text3;
    public Text copy3;
    public Text text4;
    public Text copy4;
    public Text rank1;
    public Text rank2;
    public Text rank3;
    public Text rank4;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text1.text
            = copy1.text;
        text2.text
            = copy2.text;
        text3.text
            = copy3.text;
        text4.text
            = copy4.text;
    }
}
