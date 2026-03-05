using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gudingfdpt : MonoBehaviour
{
    public Transform wz; 
    private bool canMove = false;
    public float speed = 1.0f;
    public Vector3 moveTarget;

    void Start()
    {
        
    }
    public void shangshen()
    {
        canMove = true;
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
