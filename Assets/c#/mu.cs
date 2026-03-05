using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mu : MonoBehaviour
{
    diren sw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("diren"))
        {
            if (collision.gameObject == this.transform.root.gameObject) return;// 防止技能碰撞体误伤主角自己

            Debug.Log("攻");
            diren sw = collision.GetComponent<diren>();
            if (sw != null)
            {
                sw.shouji();
            }
        }
    }
}
