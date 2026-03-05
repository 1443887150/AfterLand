using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheng : MonoBehaviour
{
public Transform wz; // 在Inspector拖入目标点

private Vector3 initialPosition;
public float moveSpeed = 5.0f;
private bool movingToTarget = false;
private bool movingToInitial = false;
private Coroutine returnCoroutine;

void Start()
{
    initialPosition = transform.position;
}

void Update()
{
    if (movingToTarget && wz != null)
    {
        transform.position = Vector3.MoveTowards(transform.position, wz.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wz.position) < 0.01f)
        {
            transform.position = wz.position;
            movingToTarget = false;
        }
    }
    else if (movingToInitial)
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
        {
            transform.position = initialPosition;
            movingToInitial = false;
        }
    }
}

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.collider.CompareTag("wanjia"))
    {
        movingToTarget = true;
        movingToInitial = false;
    }
}

private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.collider.CompareTag("wanjia"))
    {
        // 如果有旧的协程，先停止
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        // 启动新的协程
        returnCoroutine = StartCoroutine(ReturnToInitialAfterDelay(1f));
        movingToTarget = false;
    }
}

private IEnumerator ReturnToInitialAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    movingToInitial = true;
    returnCoroutine = null;
}
}
