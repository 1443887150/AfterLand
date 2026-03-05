using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shang : MonoBehaviour
{
    public Animator sgj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            sgj.SetTrigger("sgj");
        }
    }
}
