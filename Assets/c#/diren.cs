using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diren : MonoBehaviour
{
    public Animator dr;
    float dqxue,zongxue=100f;
    public AudioSource swyy;
    public AudioSource yy;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        dqxue=zongxue;
    }

    // Update is called once per frame
    void Update()
    {
        if (dqxue <= 0 && !isDead)
        {
            isDead = true;
            swyy.Play();
            dr.SetTrigger("siw");
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestroyAfterDelay(1f));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    public void shouji()
    {
        yy.Play();
        dr.SetTrigger("siw");
        dqxue-=100f;
    }
}
