using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间
public class tuichuyxi : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnExitButtonClicked);
        }
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
