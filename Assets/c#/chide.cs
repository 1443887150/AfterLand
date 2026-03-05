using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class chide : MonoBehaviour
{
    public int chi=0,chilan=0,chihon=0;
    public Text chitext,chilans,chihons;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("chi"))
        {
            Destroy(collision.gameObject);
            chi++;
            chitext.text="木元素球"+chi;
        }
        if (collision.gameObject.CompareTag("chilan"))
        {
            Destroy(collision.gameObject);
            chilan++;
            chilans.text="水元素球"+chilan;
        }
        if (collision.gameObject.CompareTag("chihon"))
        {
            Destroy(collision.gameObject);
            chihon++;
            chihons.text="火元素球"+chihon;
        }
    }
    


}
