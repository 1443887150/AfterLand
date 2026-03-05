using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class 退出设置 : MonoBehaviour
{
    public GameObject menuList; // 菜单对象
    public Button closeButton; // 关闭按钮

    void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseMenu);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuList.SetActive(!menuList.activeSelf); // 按一次显示，再按一次关闭
        }
    }

    void CloseMenu()
    {
        menuList.SetActive(false);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
