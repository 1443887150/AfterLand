using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yinxian : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wanjia") && sr != null)
        {
            sr.sortingOrder = -10; // 设为负数，渲染在后面
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("wanjia") && sr != null)
        {
            sr.sortingOrder = 2; // 恢复默认
        }
    }
}
