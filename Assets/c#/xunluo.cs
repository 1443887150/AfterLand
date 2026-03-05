using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class xunluo : MonoBehaviour
{

    [Header("巡逻基础配置")]
    public Transform[] patrolPoints; // 巡逻点数组（在Inspector拖入空物体即可）
    public float moveSpeed = 2f;     // 巡逻移动速度
    public float stopTime = 1f;      // 到达巡逻点后的暂停时间（秒）
    public bool isLoopPatrol = true; // 是否循环巡逻（false则到最后一个点停止）

    [Header("玩家检测配置（可选）")]
    public Transform player;         // 玩家Transform（可空，不拖入则关闭检测）
    public float detectRange = 3f;   // 检测玩家的视野范围
    public float detectAngle = 90f;  // 检测角度（90度为正前方左右45度）
    public LayerMask obstacleLayer;  // 障碍物图层（避免穿墙检测玩家）

    [Header("组件引用")]
    public Rigidbody2D rb;           // NPC的Rigidbody2D（自动获取）
    public Animator anim;            // NPC的动画组件（可选，不拖入则关闭动画）

    // 私有状态变量
    private int currentPointIndex;   // 当前目标巡逻点索引
    private float stopTimer;         // 到达点位后的暂停计时器
    private bool isPatrolling;       // 是否正在巡逻
    private bool isStopping;         // 是否在点位暂停

    void Start()
    {
        // 自动获取组件，无需手动拖入（如果Inspector已拖入则优先使用）
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();

        // 初始化巡逻状态
        if (patrolPoints != null && patrolPoints.Length >= 2)
        {
            isPatrolling = true;
            currentPointIndex = 0;
            stopTimer = 0;
            isStopping = false;
            // 初始朝向第一个巡逻点
            FlipToTarget(patrolPoints[currentPointIndex].position);
        }
        else
        {
            Debug.LogWarning("请在Inspector面板为NPC添加至少2个巡逻点！", this);
            isPatrolling = false;
        }

        // 刚体基础设置（避免物理异常）
        rb.gravityScale = 0; // 巡逻NPC一般关闭重力（地面NPC）
        rb.freezeRotation = true; // 冻结旋转，避免NPC歪掉
    }

    void Update()
    {
        // 优先检测玩家：发现玩家则停止巡逻
        if (player != null && IsPlayerInSight())
        {
            StopPatrol(); // 停止巡逻
            return;
        }

        // 未发现玩家，执行巡逻逻辑
        if (isPatrolling && patrolPoints.Length >= 2)
        {
            PatrolLogic();
        }

    }

    // 核心巡逻逻辑：定点往返、暂停、切换点位
    void PatrolLogic()
    {
        // 如果正在暂停，执行计时逻辑
        if (isStopping)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopTime)
            {
                // 暂停结束，切换下一个巡逻点
                stopTimer = 0;
                isStopping = false;
                SwitchNextPatrolPoint();
            }
            return;
        }

        // 移动到当前目标巡逻点
        Transform targetPoint = patrolPoints[currentPointIndex];
        // 计算目标位置（2D游戏忽略Z轴，保持NPC自身Z轴）
        Vector2 targetPos = new Vector2(targetPoint.position.x, targetPoint.position.y);
        // 匀速移动到目标点（Vector2.MoveTowards：匀速、不超距）
        Vector2 moveDir = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        rb.velocity = new Vector2(moveDir.x - transform.position.x, moveDir.y - transform.position.y) / Time.deltaTime;

        // 朝向目标点（翻转NPC图片，适配横版游戏左右翻转）
        FlipToTarget(targetPos);

        // 判断是否到达目标巡逻点（距离小于0.1则视为到达）
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            // 到达点位，停止移动并开始暂停
            rb.velocity = Vector2.zero;
            isStopping = true;
        }
    }

    // 切换下一个巡逻点（到达最后一个点则折返）
    void SwitchNextPatrolPoint()
    {
        // 索引+1，指向下一个点
        currentPointIndex++;

        // 判断是否超出巡逻点范围
        if (currentPointIndex >= patrolPoints.Length)
        {
            if (isLoopPatrol)
            {
                // 循环巡逻：折返，从最后一个点往回走
                System.Array.Reverse(patrolPoints);
                currentPointIndex = 1; // 从第二个点开始（避免重复停在最后一个点）
            }
            else
            {
                // 不循环：停止巡逻
                isPatrolling = false;
                return;
            }
        }
    }

    // 朝向目标点翻转（2D横版游戏核心：左右翻转NPC）
    void FlipToTarget(Vector2 targetPos)
    {
        // 计算NPC与目标点的水平方向差
        float dirX = targetPos.x - transform.position.x;
        if (dirX != 0)
        {
            // 翻转本地缩放：x轴正为右，负为左（适配你之前主角的缩放逻辑：0.75/-0.75）
            transform.localScale = new Vector3(Mathf.Sign(dirX) * Mathf.Abs(transform.localScale.x), 
                                               transform.localScale.y, 
                                               transform.localScale.z);
        }
    }

    // 玩家视野检测：判断是否在视野范围+角度内，且无障碍物阻挡
    bool IsPlayerInSight()
    {
        // 玩家为空则直接返回false
        if (player == null) return false;

        // 1. 第一步：判断玩家是否在检测距离内
        float disToPlayer = Vector2.Distance(transform.position, player.position);
        if (disToPlayer > detectRange) return false;

        // 2. 第二步：判断玩家是否在检测角度内
        Vector2 dirToPlayer = (player.position - transform.position).normalized; // 指向玩家的方向
        float angleToPlayer = Vector2.Angle(transform.right, dirToPlayer);      // NPC正前方与玩家的夹角
        // transform.right：NPC本地右方向（即面朝方向，适配横版左右翻转）
        if (angleToPlayer > detectAngle / 2) return false;

        // 3. 第三步：判断NPC与玩家之间是否有障碍物（射线检测，避免穿墙）
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, disToPlayer, obstacleLayer);
        if (hit.collider != null) return false; // 有障碍物则检测失败

        // 满足所有条件：发现玩家
        return true;
    }

    // 停止巡逻方法（发现玩家/手动停止时调用）
    public void StopPatrol()
    {
        isPatrolling = false;
        isStopping = false;
        stopTimer = 0;
        rb.velocity = Vector2.zero; // 立即停止移动
    }

    // 继续巡逻方法（玩家离开视野后可调用，拓展用）
    public void ContinuePatrol()
    {
        if (patrolPoints != null && patrolPoints.Length >= 2)
        {
            isPatrolling = true;
        }
    }

    // 更新动画参数（适配你之前的Animator逻辑，布尔值控制行走/待机）

    // Gizmos绘制巡逻点/检测范围（场景视图可视化，方便调试）
    void OnDrawGizmos()
    {
        // 绘制巡逻点和巡逻路径
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                // 绘制巡逻点（球体）
                Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);
                // 绘制巡逻路径（线段）
                if (i < patrolPoints.Length - 1)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
            }
        }

        // 绘制玩家检测范围（扇形）
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        // 绘制检测角度的左右边界
        Vector2 leftDir = Quaternion.Euler(0, 0, detectAngle / 2) * transform.right;
        Vector2 rightDir = Quaternion.Euler(0, 0, -detectAngle / 2) * transform.right;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + leftDir * detectRange);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rightDir * detectRange);
    }
}