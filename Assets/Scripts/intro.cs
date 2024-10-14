using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    public GameObject bp;
    public GameObject logo;
    public GameObject introText;
    Animator bpAnim;

    void Start()
    {
        bpAnim = bp.GetComponent<Animator>();
        StartCoroutine(showImg());
    }

    void Update()
    {
        
    }

    IEnumerator showImg()
    {
        yield return new WaitForSeconds(2.5f);
        bpAnim.SetInteger("bp", 1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
