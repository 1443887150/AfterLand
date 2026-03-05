using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc1 : MonoBehaviour
{
    public gudingfdpt pt,pt1,pt2,pt3,pt4,pt5,pt6,pt7;
    public GameObject duihua;
    public Text kuang;
    public string npc1s;
    bool wanjianpc;
       void Start()
    {
        duihua.SetActive(false);
        wanjianpc=false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wanjia"))
        {
            Debug.Log("碰到");
            wanjianpc = true;
            kuang.text = npc1s;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wanjia"))
        {
            Debug.Log("离开");
            duihua.SetActive(false);
            wanjianpc = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wanjianpc)
        {
            duihua.SetActive(true);
            wanjianpc=true;
            pt.shangshen();
            pt1.shangshen();
            pt2.shangshen();
            pt3.shangshen();
            pt4.shangshen();
            pt5.shangshen();
            pt6.shangshen();
            pt7.shangshen();
        }
    }
}
