using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class cs2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wanjia"))
        {
            // 进入下一关（假设下一关场景名为"NextLevel"，请根据实际场景名修改）
            SceneManager.LoadScene("lv3");
        }
    }
}
