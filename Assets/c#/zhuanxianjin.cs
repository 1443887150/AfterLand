using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhuanxianjin : MonoBehaviour
{
    public float zhuansu = 90f;
    // 围绕父物体的半径
    public float radius = 2f;
    // 当前旋转角度
    private float angle = 0f;
    // 父物体引用
    private Transform fulei;

    void Start()
    {
        // 获取父物体引用
        fulei = transform.parent;
        // 计算初始角度
        if (fulei != null)
        {
            Vector3 offset = transform.position - fulei.position;
            angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        }
    }

    void Update()
    {
        if (fulei == null) return;
        // 每帧增加角度
        angle += zhuansu * Time.deltaTime;
        // 保证角度在0~360度
        if (angle > 360f) angle -= 360f;
        // 计算新位置
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
        transform.position = fulei.position + offset;
    }
}
