using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zuoyikaiguan : MonoBehaviour
{
    public Transform wz; 
    private bool canMove = false;
    public float speed = 1.0f;
    private int jgcishu = 0;
    public Vector3 moveTarget;

    void Start()
    {
        
    }
    public void jiguan()
    {
        jgcishu++;
        if (jgcishu == 2)
        {
            canMove = true;
        }
    }

    void Update()
    {
        if (canMove)
        {
             transform.position = Vector3.MoveTowards(transform.position, wz.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, wz.position) < 0.05f)
            {
                canMove = false; // 到达目标后停止
            }
        }
    }
}
