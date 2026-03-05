using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fudonpintai : MonoBehaviour
{
    public Transform wz, wz1; // 平台移动的两个端点
    private Transform yidon;  // 当前目标点
    public float sdu;        // 平台移动速度
    private GameObject wanjiayd = null; // 当前在平台上的玩家对象
    private Vector3 ptweizhi;            // 上一帧平台的位置

    void Start()
    {
        yidon = wz; // 初始目标点为wz
        ptweizhi = transform.position; // 记录初始位置
    }

    // 每帧调用，控制平台移动和玩家跟随
    void Update()
    {
        // 到达wz点后，目标切换到wz1
        if (Vector2.Distance(transform.position, wz.position) < 0.1f)
        {
            yidon = wz1;
        }
        // 到达wz1点后，目标切换回wz
        if (Vector2.Distance(transform.position, wz1.position) < 0.1f)
        {
            yidon = wz;
        }
        // 平台向目标点移动
        Vector3 oldPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, yidon.position, sdu);
        // 如果有玩家在平台上，让玩家跟随平台的位移
        if (wanjiayd != null)
        {
            Vector3 delta = transform.position - ptweizhi; // 平台本帧的位移
            wanjiayd.transform.position += delta;         // 玩家同步移动
        }
        ptweizhi = transform.position; // 更新平台位置
    }

    // 玩家上平台时，记录玩家对象
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wanjia"))
        {
            wanjiayd = collision.gameObject;
        }
    }

    // 玩家离开平台时，取消跟随
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wanjia"))
        {
            wanjiayd = null;
        }
    }
}
