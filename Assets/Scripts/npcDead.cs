using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class npcDead : MonoBehaviour
{
    Vector2 startingPos;
    Animator animator;
    public bool navMeshState=true;
    GameObject canvas;
    button buttonOption;
    bool goverPanel = false;
    GameObject timeObject;
    public static soundsManager au;

    void Start()
    {
        startingPos = transform.position;
        canvas = GameObject.Find("Canvas1");
        animator = GetComponent<Animator>();
        buttonOption = canvas.GetComponent<button>();
        timeObject = canvas.transform.GetChild(1).gameObject;
        au = GameObject.Find("GameManager").GetComponent<soundsManager>();
    }

    void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("goverPanel:" + goverPanel);
        /*if (!animator.GetBool("Run") && collision.CompareTag("Player") && !goverPanel)
        {
            buttonOption.GameOver();
            StartCoroutine(gOverActivity());
        }
        else if (animator.GetBool("Run") && collision.CompareTag("Player"))
        {
            StartCoroutine(deadAnim());
        } */
        if (collision.CompareTag("Player"))
        {
            if (!animator.GetBool("Run") && !goverPanel)
            {
                au.playSoundWithStop(5);
                buttonOption.GameOver();
                buttonOption.restartText();
                timeObject.GetComponent<timeManagment>().coutDownCancel();
                StartCoroutine(gOverActivity());
            }
            else if (animator.GetBool("Run"))
            {
                au.playSound(4);
                StartCoroutine(deadAnim());
            }
        }
    }

    IEnumerator gOverActivity()
    {
        goverPanel = true;
        yield return new WaitForSeconds(3f);
        goverPanel = false;
        print("Game Over");
        Time.timeScale = 0f;
    }
    
    IEnumerator deadAnim()
    {
        //NavMeshAgent nvm=GetComponent<NavMeshAgent>();
        //npcAnimControl npa = GetComponent<npcAnimControl>();
        navMeshState = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<npcAnimControl>().enabled = false;
        //print("enabled false:"+gameObject.name);
        animator.SetTrigger("dead");
        yield return new WaitForSeconds(0.5f);
        transform.position = startingPos;
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("born");
        //print("enabled true:" + gameObject.name);
        yield return new WaitForSeconds(npcFollow.ghostTime);
        animator.SetBool("Run", false);
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<npcAnimControl>().enabled = true;
        //print("enabled true:" + gameObject.name);
        navMeshState = true;
    }
}
