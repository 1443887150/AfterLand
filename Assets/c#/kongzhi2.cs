using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kongzhi2 : MonoBehaviour
{
    
    
    int tiaoci = 2; // 最大空中/蹬墙跳次数
    int dqtiaoci = 0; // 当前剩余次数
    public Rigidbody2D yd;
    float yidon=3.5f,tiao=6f,sd;
    public float isg,dqtjl = 0.6f,dqtld = 0.5f;
    public AudioSource yy;
    public AudioSource attackAudio,tiaoyin; // 攻击音效
    public Animator gj;
    Animator zhujue;
    private zidan1 zidan;
    bool pao1,shidimian,isbofang,tiaosd,tiao2,dqt,zuoqiang, youqiang;
    public LayerMask dimian;//材质
    
    enum ss{d123,benpao,tiao,luo,dq};
    
    // 新增变量
    bool lastGrounded = false; // 记录上一帧是否在地面
    bool jumpPressedThisFrame = false; // 记录本帧是否主动跳跃
    
    void Start()
    {
        yd=GetComponent<Rigidbody2D>();
        zhujue=GetComponent<Animator>();
        zidan = FindObjectOfType<zidan1>();
    }

    // Update is called once per frame
    void Update()
    {
        sd=Input.GetAxisRaw("Horizontal");
        // 左右墙体检测
        zuoqiang = Physics2D.Raycast(transform.position, Vector2.left, dqtjl, dimian);
        youqiang = Physics2D.Raycast(transform.position, Vector2.right, dqtjl, dimian);
        
        // 地面检测
        shidimian = Physics2D.BoxCast(
    transform.position, // 中心
    new Vector2(0.5f, 0.1f), // 检测盒子的宽高
    0f, // 旋转角度
    Vector2.down, // 方向
    1.2f, // 距离
    dimian // 层
);
        // 检测平台掉落：上一帧在地面，这一帧不在地面，且本帧没有主动跳跃
        if (lastGrounded && !shidimian && !jumpPressedThisFrame) {
            dqtiaoci = 1; // 平台掉落后只允许一次空中跳
        }
        lastGrounded = shidimian;
        jumpPressedThisFrame = false; // 每帧重置

        caozuo();
        dh();
        shenyin();
        //transform.Rotate(0f,180f,0f);
    }
    void caozuo()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (zidan.yuansu == 0)
            gj.SetTrigger("gongji");   // 木元素攻击动画
        else if (zidan.yuansu == 1)
            gj.SetTrigger("gongjiShui"); // 水元素攻击动画
        else if (zidan.yuansu == 2)
            gj.SetTrigger("gongjiHuo"); 
            
            attackAudio.Play(); // 播放攻击音效
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (zidan.yuansu == 0)
            gj.SetTrigger("shanggong");   // 木元素攻击动画
        else if (zidan.yuansu == 1)
            gj.SetTrigger("shanggongShui"); // 水元素攻击动画
        else if (zidan.yuansu == 2)
            gj.SetTrigger("shanggongHuo"); 
            
            attackAudio.Play(); // 播放攻击音效
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (zidan.yuansu == 0)
            gj.SetTrigger("xiagong");   // 木元素攻击动画
        else if (zidan.yuansu == 1)
            gj.SetTrigger("xiagongShui"); // 水元素攻击动画
        else if (zidan.yuansu == 2)
            gj.SetTrigger("xiagongHuo"); 
            
        attackAudio.Play(); // 播放攻击音效
        }
        if (Input.GetKey(KeyCode.D))
        {
            yd.velocity=new Vector2(yidon,yd.velocity.y);
            transform.localScale=new Vector2(0.2019f,0.2019f);
            pao1=true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            yd.velocity=new Vector2(-yidon,yd.velocity.y);
            transform.localScale=new Vector2(-0.2019f,0.2019f);
            pao1=true;
        }
        else {
            pao1=false;
            yd.velocity = new Vector2(0, yd.velocity.y); // 松开AD后立即停止
        }
        // 主动地面起跳
        if (Input.GetKeyDown(KeyCode.Space) && shidimian)
        {
            if (attackAudio != null)
            {
                tiaoyin.Play(); // 播放跳跃音效
            }
            yd.velocity = new Vector2(yd.velocity.x, tiao);
            tiaosd = true;
            tiao2 = true;
            dqtiaoci = 1; // 地面起跳后只允许一次空中跳
            jumpPressedThisFrame = true; // 标记本帧主动跳跃
        }
        // 蹬墙跳（空中贴墙）
        if (Input.GetKeyDown(KeyCode.Space) && !shidimian && (zuoqiang || youqiang))
        {
            if (attackAudio != null)
            {
                tiaoyin.Play(); // 播放跳跃音效
            }
            float wallDir = zuoqiang ? 1f : -1f;
            yd.velocity = new Vector2(wallDir * yidon * 0.8f, dqtld);
            tiao2 = false;
        }
        // 空中二段跳（不靠墙也能跳）
        if (Input.GetKeyDown(KeyCode.Space) && !shidimian && dqtiaoci > 0 && !(zuoqiang || youqiang))
        {
            if (attackAudio != null)
            {
                tiaoyin.Play(); // 播放跳跃音效
            }
            yd.velocity = new Vector2(yd.velocity.x, tiao);
            dqtiaoci--;
        }
        // 落地时重置
        if (yd.velocity.y == 0 && shidimian)
        {
            tiaosd = true;
            dqtiaoci = 1; // 落地后只允许一次空中跳
        }
        if (yd.velocity.y == 0 && shidimian)
        {
            tiaosd=true;
            dqt = true; // 落地后重置蹬墙跳
        }
        if (yd.velocity.y<0)
        {
            tiaosd=false;
        }
        if (tiaosd == false)
        {
            yd.velocity +=new Vector2(0,-0.02f);
        }
        
        
    }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         shidimian = true;
    //     }
    //     if (collision.gameObject.CompareTag("WallLeft"))
    //     {
    //         zuoqiang = true;
    //     }
    //     if (collision.gameObject.CompareTag("WallRight"))
    //     {
    //         youqiang = true;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         shidimian = false;
    //     }
    //     if (collision.gameObject.CompareTag("WallLeft"))
    //     {
    //         zuoqiang = false;
    //     }
    //     if (collision.gameObject.CompareTag("WallRight"))
    //     {
    //         youqiang = false;
    //     }
    // }
    void FlipToTarget(Vector2 targetPos)
{
    // 计算当前物体与目标点的水平方向差
    float dirX = targetPos.x - transform.position.x;
    
    // 只有方向改变时才翻转，避免重复赋值导致抖动
    if (dirX > 0 && transform.localScale.x < 0)
    {
        // 目标在右侧，朝右翻转（X轴为正）
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    else if (dirX < 0 && transform.localScale.x > 0)
    {
        // 目标在左侧，朝左翻转（X轴为负）
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
}
    void dh()
    {
        // if (sd != 0)
        // {
        //     zhujue.SetBool("pao1",true);
        // }
        // else
        // {
        //     zhujue.SetBool("pao1",false);
        // }
        ss wj;
        if (sd != 0)
        {
            wj=ss.benpao;
        }
        else
        {
            wj = ss.d123;
        }
        if (yd.velocity.y > 0&&!shidimian)
        {
            wj=ss.tiao;
        }
        else if(yd.velocity.y < 0&&!shidimian)
        {
            wj=ss.luo;
        }
        // 空中贴墙时，切换到dengqiang
        if (!shidimian && (zuoqiang || youqiang) && Mathf.Abs(yd.velocity.y) < 0.1f)
        {
            wj=ss.dq;
        }
        zhujue.SetInteger("intlei",(int)wj);
    }
    void shenyin()
    {
        if (pao1&& shidimian)
        {
            if (!isbofang) // 仅在音频未播放时调用Play()
            {
                yy.Play();
                isbofang = true;
            }
        }
        else
        {
            if (isbofang) // 仅在音频播放时调用Stop()
            {
                yy.Stop();
                isbofang = false;
            }
        }
    }
    
    
    public void AddAirJump()
{
    dqtiaoci++;
    dqtiaoci++;
    Debug.Log("空中攻击敌人，获得一次二段跳");
}
public bool IsGrounded()
{
    return shidimian;
}
    
    void OnDrawGizmos()//c#绘制组件
    {
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y-isg));
    }
}
