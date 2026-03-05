using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class yingliang : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider != null)
        {
            slider.value = AudioListener.volume;
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        AudioListener.volume = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
