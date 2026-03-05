using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dansheji : MonoBehaviour
{
    public float sd,sh,gjfw;
    Rigidbody2D rb2d;
    Vector3 wzhi;
    diren ss1;
    
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right*sd;
        wzhi=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dis=(transform.position-wzhi).sqrMagnitude;
        if (dis > gjfw)
        {
            Destroy(gameObject);
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("攻");
        if (collision.CompareTag("diren"))
        {
            ss1 = collision.GetComponent<diren>();
            ss1.shouji();
        }
    }
}
