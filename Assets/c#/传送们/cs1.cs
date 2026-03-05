using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ← 必须加上这行

public class cs1 : MonoBehaviour
{
    public AudioSource portalAudio; // 拖拽AudioSource到这里
    public float delay = 2f; // 音乐播放时长，按实际音乐长度设置

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wanjia"))
        {
            if (portalAudio != null)
            {
                portalAudio.Play();
                StartCoroutine(LoadNextSceneAfterDelay());
            }
            else
            {
                SceneManager.LoadScene("lv2");
            }
        }
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("lv2");
    }
}
