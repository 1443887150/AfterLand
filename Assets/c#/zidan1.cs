using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class zidan1 : MonoBehaviour
{
    public Animator zhujuedh;
    public Text selectText;
    public Image elementImage; // 用于显示元素图片
    public Sprite[] elementSprites; // 三种元素图片，顺序和typeNames一致
    public string[] typeNames;
    public int yuansu;
    public float skillCD;
    private float skillTimer;
    public GameObject muImage;
public GameObject shuiImage;
public GameObject huoImage;
    private chide zd1;
    public GameObject gjfw;

    void Start()
    {
        UpdateElementImage();
        zd1 = FindObjectOfType<chide>();
        if (selectText != null && typeNames != null && typeNames.Length > yuansu)
        selectText.text = " " ;
    else
        Debug.LogWarning("selectText未赋值或typeNames配置有误！");
    }

    void Update()
    {
        skillTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
    {
        // 先判断elementSprites长度，防止除零
        int count = 3; // 你有3个元素
        yuansu = (yuansu + 1) % count;
        UpdateElementImage();
        // 如果你还想切换elementImage.sprite，确保elementSprites有内容
        if (elementImage != null && elementSprites != null && elementSprites.Length == count)
            elementImage.sprite = elementSprites[yuansu];
    }

        if (skillTimer <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            if (zhujuedh != null && zd1 != null)
            {
                if (yuansu == 0 && zd1.chi > 0)
                {
                    zhujuedh.SetTrigger("skillMu");
                    zd1.chi--;
                    if (zd1.chitext != null) zd1.chitext.text = "" + zd1.chi;
                }
                else if (yuansu == 1 && zd1.chilan > 0)
                {
                    zhujuedh.SetTrigger("skillShui");
                    zd1.chilan--;
                    if (zd1.chilans != null) zd1.chilans.text = "" + zd1.chilan;
                }
                else if (yuansu == 2 && zd1.chihon > 0)
                {
                    zhujuedh.SetTrigger("skillHuo");
                    zd1.chihon--;
                    if (zd1.chihons != null) zd1.chihons.text = "" + zd1.chihon;
                }
            }
            skillTimer = skillCD;
        }
    }
    void UpdateElementImage()
{
    muImage.SetActive(yuansu == 0);
    shuiImage.SetActive(yuansu == 1);
    huoImage.SetActive(yuansu == 2);
}
    public void gongjiff()
    {
        gjfw.SetActive(true);
    }
    public void gongjiqx()
    {
        gjfw.SetActive(false);
    }
}
