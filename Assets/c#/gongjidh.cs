using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongjidh : MonoBehaviour
{
    public GameObject dh; // 在Inspector拖拽你的技能碰撞体子物体
    
    public void ks()
    {
        if (dh != null)
            dh.SetActive(true);
    }

    // 动画事件：结束攻击判定
    public void js()
    {
        if (dh != null)
            dh.SetActive(false);
    }
}
