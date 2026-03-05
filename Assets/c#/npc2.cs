using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npc2 : MonoBehaviour
{
    zuoyikaiguan kg;
    public GameObject duihua;
    public Text kuang;
    public string npc2s;
    bool wanjianpc;
       void Start()
    {
        duihua.SetActive(false);
        wanjianpc=false;
        kg = FindObjectOfType<zuoyikaiguan>();
    }

    // Update is called once per frame
    void Update()
{
    if (wanjianpc && Input.GetKeyDown(KeyCode.F))
    {
        duihua.SetActive(true);
        kg.jiguan();
        GetComponent<Collider2D>().enabled = false; // 关闭碰撞体
        Destroy(gameObject, 1f);
    }
}

void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("wanjia"))
    {
        Debug.Log("碰到");
        kuang.text = npc2s;
        wanjianpc = true; // 只有靠近时才允许对话
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
}
