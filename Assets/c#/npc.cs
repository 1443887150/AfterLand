using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc : MonoBehaviour
{
    public GameObject duihua;
    public Text kuang;
    public string npc1;
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
            kuang.text = npc1;
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
        if (wanjianpc )
        {
            duihua.SetActive(true);
            wanjianpc=true;
        }
    }
}
