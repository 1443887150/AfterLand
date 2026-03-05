using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shui : MonoBehaviour
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
        Debug.Log("攻");
        sw = collision.GetComponent<diren>();
        if (sw != null)
        {
            sw.shouji();
        }
    }
}
