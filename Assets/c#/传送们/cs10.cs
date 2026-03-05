using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs10 : MonoBehaviour
{
    private static List<cs10> allDoors = new List<cs10>();
    // 防止刚传送后立刻再次触发
    // 记录上一次被传送的玩家对象，防止刚传送后立即再次触发
    private static Transform duixiang = null;
    // 传送冷却时间（秒），防止玩家在门之间无限循环传送
    private static float lenque = 0.2f;
    // 上一次传送的时间戳
    private static float shijian = -1f;

    // 当脚本实例化时（门生成时）自动加入门列表
    private void Awake()
    {
        allDoors.Add(this);
    }
    // 当门被销毁时自动从门列表移除，防止引用无效对象
    private void OnDestroy()
    {
        allDoors.Remove(this);
    }

    // 当有物体进入门的2D触发器时会调用此方法
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 判断进入门的物体是否是玩家（Tag为"wanjia"，需在Inspector设置）
        if (other.CompareTag("wanjia"))
        {
            // 如果刚刚传送过，且冷却时间未到，则不再传送，防止无限循环
            if (duixiang == other.transform && Time.time - shijian < lenque)
                return;
            // 遍历所有门，找到另一个门（不是自己）
            foreach (var door in allDoors)
            {
                if (door != this)
                {
                    // 将玩家的位置设置为另一个门的位置，实现瞬移
                    other.transform.position = door.transform.position;
                    // 记录本次传送的玩家和时间，防止刚传送后再次触发
                    duixiang = other.transform;
                    shijian = Time.time;
                    break; // 只传送到第一个找到的另一个门
                }
            }
        }
    }
}
