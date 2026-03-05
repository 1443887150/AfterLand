using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siwangdonghua : MonoBehaviour
{
    Animator sw;
    Vector2 wj;
    Rigidbody2D wanjia;

    void Start()
    {
        sw=GetComponent<Animator>();
        wj=transform.position;
        wanjia=GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        
    }
    void swdh()
    {
        sw.SetTrigger("kaiguan");
    }
    
}
