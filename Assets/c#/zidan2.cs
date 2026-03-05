using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class zidan2 : MonoBehaviour
{
    float jg=0.5f;
    public float faSheLiDu=5f;
    public GameObject zidan1;
    public Transform shu;
    public chide zd1;
    chide ss1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nengliang = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nengliang.z = 0; // 2D游戏要把Z轴设为0，避免深度问题
        Vector3 dir = nengliang - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (jg <= 0 && zd1.chihon > 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // 生成子弹
                GameObject bullet = Instantiate(zidan1, shu.position, transform.rotation);
                
                // 给子弹添加发射力
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // 让子弹沿着炮口的向右方向（transform.right）飞
                    rb.AddForce(transform.right * faSheLiDu, ForceMode2D.Impulse);
                }
                else
                {
                    Debug.LogError("子弹预制体缺少 Rigidbody2D 组件！", bullet);
                }
                zd1.chihon--;
                // 更新UI显示
                zd1.chihons.text = "火元素:" + zd1.chihon;

                // 重置冷却
                jg = 0.5f;
            }
        }
        else
        {
            // 冷却倒计时
            jg -= Time.deltaTime;
        }
        
    }
}
