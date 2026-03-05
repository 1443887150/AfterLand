using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuhuo : MonoBehaviour
{
    public static Vector3 fuhuoPos;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wanjia"))
        {
            fuhuoPos = transform.position;
            Debug.Log("复活点已更新为：" + fuhuoPos);
        }
    }
    
}
