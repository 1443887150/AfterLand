using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tguan : MonoBehaviour
{
    private bool canEnter = false;

    void Update()
    {
        // 如果玩家在目标上并按下F键，切换到lv2关卡
        if (canEnter && Input.GetKeyDown(KeyCode.F))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("lv2");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("zhujue"))
        {
            canEnter = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("zhujue"))
        {
            canEnter = false;
        }
    }
}
