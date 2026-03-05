using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diaoci : MonoBehaviour
{
    public Transform quyu; // 触发区域（空物体或带Collider2D的物体）
    public string biao = "wanjia"; // 玩家Tag
    public Transform wzhi; // 要移动到的位置
    public float moveSpeed = 10f;
    private bool isMoving = false;

    void Update()
{
    // 检查玩家是否在触发区域内
    Collider2D[] hits = Physics2D.OverlapCircleAll(quyu.position, 0.1f);
    Debug.Log("检测到玩家");
    foreach (var hit in hits)
    {
        if (hit.CompareTag(biao))
        {
            isMoving = true;
            break;
        }
    }

    // 平滑移动
    if (isMoving && wzhi != null)
    {
        transform.position = Vector3.MoveTowards(transform.position, wzhi.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wzhi.position) < 0.01f)
        {
            isMoving = false;
        }
    }
}
}
