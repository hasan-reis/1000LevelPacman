using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcFollow : MonoBehaviour
{
    public static float npcSpeed=1.4f;
    NavMeshAgent nm;
    Animator anim;
    public float prisonTime;
    public bool inPrison = true;
    public bool isGhost;
    Transform[] runPoints=new Transform[4];
    public static float ghostTime = 5f;
    public bool navMeshState;
    public bool ghostState=false;
    Transform player;
    //public static float enemySpeed;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nm.updateRotation = false;
        nm.updateUpAxis = false;
        player = GameObject.Find("pacman_0").transform;
        ghostTime = 5f;

        string nameRunPoint;
        if (GameManager.levelNumber % 2 != 0) nameRunPoint = "runPointsl1";
        else nameRunPoint = "runPointsl2";

        GameObject mainRunPointsObject = GameObject.Find(nameRunPoint);
        for (int i = 0; i < mainRunPointsObject.transform.childCount; i++)
        {
            runPoints[i] = mainRunPointsObject.transform.GetChild(i);
        }

        //string levelStr = GameObject.Find("levelManager(Clone)").transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text; //button.LevelManager
        //int level = int.Parse(levelStr.Substring(levelStr.Length - 1));

        StartCoroutine(waitInPrison());
    }

    void Update()
    {
        navMeshState = GetComponent<npcDead>().navMeshState;
        //print(gameObject.name + ":" + gameObject.GetComponent<npcFollow>().isGhost); //false
        if (!inPrison && !isGhost && navMeshState)
        {
            //print("follow:"+ gameObject.name);
            playerFollow();
        }
        else if (isGhost && navMeshState)
        {
            if (ghostState)
            {
                StartCoroutine(ghostManage());
                ghostState = false;
            }
            runGhost();

        }
        else if (!isGhost && navMeshState)
        {
            //print("turnCenter");
            nm.SetDestination(new Vector3(0.115999997f, 0.358596355f, -0.00817680731f));
        }
    }

    private void FixedUpdate()
    {
       // print("gost:" + gameObject.name + ":" + npcFollow.ghostTime);
    }

    void playerFollow()
    {
        nm.SetDestination(player.position);
    }

    public IEnumerator waitInPrison()
    {
        inPrison = true;
        yield return new WaitForSeconds(prisonTime);
        inPrison = false;
    }

    void runGhost()
    {
        anim.SetBool("Run", true);

        int targetIndex = 0;
        float maxRp = 0;

        for (int i = 0; i < runPoints.Length; i++)
        {
            float temp = Vector2.Distance(player.transform.position, runPoints[i].position);

            if (temp > maxRp)
            {
                maxRp = temp;
                targetIndex = i;
            }
        }
        nm.SetDestination(runPoints[targetIndex].position);
    }

    void returnToEnemyCase()
    {
        isGhost = false;
        ghostTime = 5f;
        anim.SetBool("Run", false);
        anim.SetBool("white", false);
    }

    IEnumerator ghostManage()
    {
        float elapsedTime = 0f;
        bool soundplay = true;

        while (elapsedTime < ghostTime)
        {
            //print("kalanTime:" + elapsedTime);
            //print("ghostTime:" + ghostTime);
            if (2 >= ghostTime - elapsedTime)
            {
                anim.SetBool("white", true);
                if (soundplay) { npcDead.au.playSound(8); soundplay = false; }
            }
                elapsedTime += Time.deltaTime;
                yield return null;
        }

        returnToEnemyCase();
    }

 //public void reduceGhostTime()
 //{
 //    StartCoroutine(IreduceGhostTime());
 //}
 //
 //IEnumerator IreduceGhostTime()
 //{
 //    yield return new WaitForSeconds(ghostTime);
 //
 //    ghostTime = 10;
 //    print("after2 ghostTime:" + ghostTime);
 //}
}