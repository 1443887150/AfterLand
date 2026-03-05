using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
{
    public float yidon = 0.02f;
    float lidi=0.2f,tiao = 5.24f,sd=0f;
    Rigidbody2D zj;
    public Animator gj;
    Animator dh;
    SpriteRenderer wanjia;
    BoxCollider2D pzt;
    public LayerMask tiao1ci;
    Collider2D selfCollider;
    enum dhlj{b123,benpaopao,qitiao,luo}
    bool tiao2ci;

    void Start()
    {
        dh=GetComponent<Animator>();
        zj=GetComponent<Rigidbody2D>();
        wanjia=GetComponent<SpriteRenderer>();
        pzt=GetComponent<BoxCollider2D>();
        selfCollider=GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        sd=Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-yidon,0,0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(yidon,0,0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && zds())
        {
            zj.velocity=new Vector2(zj.velocity.x,tiao);
        }
        donghua();
    }
    void donghua()
    {
        dhlj wanj;
        if (sd > 0f)
        {
            
            wanj = dhlj.benpaopao;
            wanjia.flipX=false;
            //dh.SetBool("zoupao",true);
        }
        else if (sd < 0f)
        {
            
            wanj = dhlj.benpaopao;
            wanjia.flipX=true;
            //dh.SetBool("zoupao",true);
        }
        else
        {
            wanj = dhlj.b123;
            //dh.SetBool("zoupao",false);
        }
        if (!zds()) 
        {
            Debug.Log("角色不在地面，开始判断跳跃/下落");
            if (zj.velocity.y > 0.1f)
            {
                wanj = dhlj.qitiao;
                Debug.Log("切换到跳跃动画，intlei=2");
            }
            else if (zj.velocity.y < -0.1f) 
            {
                wanj = dhlj.luo;
                Debug.Log("切换到跳跃动画，intlei=3"); 
            }
        }
        dh.SetInteger("intlei",(int)wanj);
    }
    bool zds()
    {
        if (pzt == null || selfCollider == null)
        {
            Debug.LogError("缺少碰撞体组件！");
            return false;
        }
        Vector2 checkCenter = new Vector2(pzt.bounds.center.x, pzt.bounds.min.y + 0.05f);  // 1. 检测中心移到角色脚底（碰撞体底部偏上0.05f）
        Vector2 checkSize = new Vector2(pzt.bounds.size.x * 0.8f, 0.1f);// 2. 缩小检测盒，避免碰到自身/左右障碍物
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            checkCenter,    // 脚底中心
            checkSize,      // 薄矩形检测盒
            0f,// 无旋转
            Vector2.down, // 向下检测
            0.1f,  // 检测距离（0.1米）
            tiao1ci
            );
             foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider != selfCollider)
            {
                Debug.Log("检测到地面：" + hit.collider.gameObject.name);
                return true;
            }
        }

        Debug.Log(" 未检测到地面（角色在空中）");
        return false;
    }
    void OnDrawGizmos()
{
    if (pzt == null) return;
        Gizmos.color = Color.red;
        Vector2 checkCenter = new Vector2(pzt.bounds.center.x, pzt.bounds.min.y - 0.05f);
        Vector2 checkSize = new Vector2(pzt.bounds.size.x * 0.6f, 0.02f);
        Gizmos.DrawWireCube(checkCenter + Vector2.down * 0.05f, checkSize);
}
}
