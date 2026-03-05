using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shexiangtou : MonoBehaviour
{
    public Transform zhujue;
    public Vector3 offset;
    public float followSpeed = 10f;
    public Collider2D cameraBounds; // PolygonCollider2D

    void LateUpdate()
    {
        if (zhujue == null || cameraBounds == null) return;

    Vector3 targetPosition = zhujue.position + offset;
    float vertExtent = Camera.main.orthographicSize;
    float horzExtent = vertExtent * Camera.main.aspect;

    Vector3 camPos = targetPosition;

    // 检查四角是否都在PolygonCollider2D内
    for (int i = 0; i < 5; i++) // 最多推5次，防止死循环
    {
        Vector2[] corners = new Vector2[]
        {
            new Vector2(camPos.x - horzExtent, camPos.y - vertExtent), // 左下
            new Vector2(camPos.x - horzExtent, camPos.y + vertExtent), // 左上
            new Vector2(camPos.x + horzExtent, camPos.y - vertExtent), // 右下
            new Vector2(camPos.x + horzExtent, camPos.y + vertExtent)  // 右上
        };

        bool allInside = true;
        foreach (var corner in corners)
        {
            if (!cameraBounds.OverlapPoint(corner))
            {
                // 推回中心点
                Vector2 closest = cameraBounds.ClosestPoint(corner);
                Vector2 offsetToCenter = (Vector2)camPos - corner;
                camPos = (Vector3)(closest + offsetToCenter);
                allInside = false;
                break; // 推一次后重新检测
            }
        }
        if (allInside) break;
    }

    camPos.z = targetPosition.z;
    transform.position = Vector3.Lerp(transform.position, camPos, followSpeed * Time.deltaTime);
}
}
