using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ui : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("SampleScene"); // 请确保关卡1已添加到Build Settings
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
