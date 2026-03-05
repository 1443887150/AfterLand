using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lie : MonoBehaviour
{
    private bool isRespawning = false;
    public Renderer rend;
    public Collider2D col;

    private void Awake()
    {
        if (rend == null) rend = GetComponent<Renderer>();
        if (col == null) col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wanjia") && !isRespawning)
        {
            isRespawning = true;
            StartCoroutine(DisappearAndRespawnSelf());
        }
    }

    private System.Collections.IEnumerator DisappearAndRespawnSelf()
    {
        yield return new WaitForSeconds(0.5f); // 0.5秒后消失
        if (rend != null) rend.enabled = false;
        if (col != null) col.enabled = false;
        yield return new WaitForSeconds(5f); // 5秒后重新出现
        if (rend != null) rend.enabled = true;
        if (col != null) col.enabled = true;
        isRespawning = false;
    }
}
