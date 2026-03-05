using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class zidan : MonoBehaviour
{
    public Transform shu; // 发射位置
    public float faSheLiDu = 10f; // 发射力度
    public float jg = 0.5f; // 发射间隔
    public GameObject zidanMu, zidanShui, zidanHuo; // 三种子弹预制体
    public UnityEngine.UI.Text selectText; // 当前选中元素UI
    int yuansu = 0; // 0=木，1=水，2=火
    string[] typeNames = {"木元素", "水元素", "火元素"};
    chide zd1; // 引用zd1脚本

    void Start()
    {
        UpdateSelectText();
    }

    void Update()
    {
        Vector3 nengliang = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nengliang.z = 0;
        Vector3 dir = nengliang - shu.position; // 用shu的位置
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shu.rotation = Quaternion.Euler(0, 0, angle); // 只让shu旋转


        // 切换元素
        if (Input.GetKeyDown(KeyCode.E))
        {
            yuansu = (yuansu + 1) % 3;
            UpdateSelectText();
        }

        // 释放选中元素
        if (jg <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (yuansu == 0 && zd1.chi > 0)
                {
                    Fire(zidanMu, ref zd1.chi, zd1.chitext, "木元素");
                }
                else if (yuansu == 1 && zd1.chilan > 0)
                {
                    Fire(zidanShui, ref zd1.chilan, zd1.chilans, "水元素");
                }
                else if (yuansu == 2 && zd1.chihon > 0)
                {
                    Fire(zidanHuo, ref zd1.chihon, zd1.chihons, "火元素");
                }
            }
        }
        else
        {
            jg -= Time.deltaTime;
        }
    }

    void Fire(GameObject bulletPrefab, ref int count, UnityEngine.UI.Text ui, string label)
    {
        GameObject bullet = Instantiate(bulletPrefab, shu.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(transform.right * faSheLiDu, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("子弹预制体缺少 Rigidbody2D 组件！", bulletPrefab);
        }
        count--;
        ui.text = label + ":" + count;
        jg = 0.5f;
    }

    void UpdateSelectText()
    {
        if (selectText != null)
        {
            selectText.text = "当前元素: " + typeNames[yuansu];
        }
    }
}
