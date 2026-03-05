using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class xuetiao : MonoBehaviour
{
    public xue xueUI; // 引用xue脚本
    Animator sw;
    Vector2 wj;
    Rigidbody2D wanjia;
    float wudiTime = 0f;
    public float dqxue,zongxue=100f;
    void Start()
    {
        sw=GetComponent<Animator>();
        wj=transform.position;
        wanjia=GetComponent<Rigidbody2D>();
        dqxue=zongxue;
        Debug.Log("xueUI是否赋值：" + (xueUI != null));
    }
    void Update()
    {
        if (xueUI != null)
        {
            xueUI.setxue((int)dqxue);
        }
        if (wudiTime > 0)
            wudiTime -= Time.deltaTime;
        if (dqxue <= 0)
        {
            swdh();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ci")
        {
            kouxue();
            kouxue();
        }
        if (collision.gameObject.CompareTag("dgj") && this.gameObject.layer == LayerMask.NameToLayer("zhujue"))
        {
        pen(collision.gameObject);
        }
    }
    void pen(GameObject rr)
    {
        
        if (rr.CompareTag("dgj"))
        {
            Debug.Log("玩家受伤，碰到的对象：" + rr.name);
            if (wudiTime <= 0f)
            {
                kouxue();
                wudiTime = 0.5f;
            }
        }
        else if (rr.CompareTag("chi"))
        {
            jiaxue();
        }
    }

    public void kouxue()
    {
        Debug.Log("扣血，当前血量：" + dqxue);
        dqxue -= 50f;
        if (dqxue < 0)
        {
            dqxue = 0;
        }
        if (xueUI != null)
        {
            xueUI.setxue((int)dqxue);
        }
    }
    public void jiaxue()
    {
        dqxue += 1f;
        if (dqxue > zongxue)
        {
            dqxue = zongxue;
        }
        if (xueUI != null)
        {
            xueUI.setxue((int)dqxue);
        }
    }
    void swdh()
    {
        sw.SetTrigger("kaiguan");
    dqxue = 100;
    // 复活到最新复活点
    if (fuhuo.fuhuoPos != Vector3.zero)
    {
        transform.position = fuhuo.fuhuoPos;
    }
    }
}
