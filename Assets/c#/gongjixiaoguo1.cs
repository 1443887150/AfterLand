using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongjixiaoguo1 : MonoBehaviour
{
    diren sw;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("攻");
        sw = collision.GetComponent<diren>();
        if (sw != null)
        {
            sw.shouji();
            kongzhi2 player = FindObjectOfType<kongzhi2>();
        if (player != null && !player.IsGrounded()) // 只在空中攻击时加跳
        {
            player.AddAirJump();
        }
        }
    }
    
}
