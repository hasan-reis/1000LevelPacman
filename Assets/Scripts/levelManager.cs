using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    public float speedDoublingAmount=1.15f;
    public static int level=1;
    GameObject player;
    public static List<GameObject> pills = new List<GameObject>();
    Transform mainPillsObject;
    int enemyIndex=0;
    public static bool createdLManagerObject=false;

    public Text levelText;
    GameObject timeManager;
    public GameObject[] prefabEnemiess;
    /*
    private void Awake()
    {
        if (!createdLManagerObject)
        {
            print("levelmanager olusturuldu");
            DontDestroyOnLoad(this.gameObject);
            createdLManagerObject = true;
        }
        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }

    void Start()
    {
        print("start");
        //levelText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        //player = transform.GetChild(1).gameObject;
        //mainPillsObject = transform.GetChild(2);

        levelText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        player = GameObject.Find("pacman_0");
        player.GetComponent<pacmanControl>().speed /=2;
        mainPillsObject = GameObject.FindWithTag("powerPill").transform;

        pillsToArray();
    }

    public void variableSettings(int scenIndex)
    {
        StartCoroutine(variableSetting(scenIndex));
    }

    IEnumerator variableSetting(int sceneIndex)
    {
        SceneManager.LoadScene(nextLevel(sceneIndex));
        yield return new WaitForSeconds(1f);

        level++;
        levelText.text = "Level " + level;

        //disabledEnemy.enemiesToArray();

        if (level % 3 == 0)
        {
            pillSettings();
            timeSettings();
        }

        if (level % 4 == 0)
        {
            npcSettings();
            playerSettings();
        }

        if (level==3 || level % 5 == 0)
            increaseNumberOfEnemies();
    }

    void npcSettings()
    {
        //hiz arttirma
        foreach (GameObject enemy in disabledEnemy.enemies)
        {
            enemy.GetComponent<NavMeshAgent>().speed *= speedDoublingAmount;
            print("enemy hiz:"+ enemy.GetComponent<NavMeshAgent>().speed);
        }
    }

    void increaseNumberOfEnemies()
    {
            GameObject newEnemy = Instantiate(prefabEnemiess[enemyIndex]);
            //disabledEnemy.newEnemyToArray(newEnemy);
            npcFollow newEnScript = newEnemy.GetComponent<npcFollow>();
            newEnemy.transform.localScale = new Vector3(0.207459867f, 0.207459867f, 0.207459867f);
            newEnScript.prisonTime = 0;
            print("enemyIndex:"+enemyIndex+" new enemy created:" + newEnemy.name);
            if (enemyIndex > 0 && enemyIndex < disabledEnemy.enemies.Count)
            {
                newEnScript.prisonTime = disabledEnemy.enemies[enemyIndex - 1].GetComponent<npcFollow>().prisonTime + 3f;
            }

            disabledEnemy.enemies.Add(newEnemy);
            enemyIndex++;

            if (enemyIndex >= 4)
                enemyIndex = 0;
    }


    void playerSettings()
    {
        if (player != null)
        {
            player.GetComponent<pacmanControl>().speed *= speedDoublingAmount;
            print("player hiz:" + player.GetComponent<pacmanControl>().speed);
            player.GetComponent<Animator>().SetBool("death", false);
        }
    }

    void timeSettings()
    {
        timeManager = GameObject.FindWithTag("timeManager");

        if (timeManager != null) ;
            //timeManager.GetComponent<timeManagment>().time += 2.5f;
    }

    void pillSettings()
    {
        print("pill len:" + pills.Count);
        if (pills.Count > 0)
        {
            Destroy(pills[0]);
            pills.RemoveAt(0);
        }
    }

    void pillsToArray()
    {
        for (int i=0; i < mainPillsObject.childCount; i++)
        {
            print("pill to array");
            pills.Add(mainPillsObject.GetChild(i).gameObject);
        }
    }

    public int nextLevel(int sceneIndex)
    {
        food.score = 0;
        food.eatenFood = 0;
        return (sceneIndex == 1) ? 2 : 1;
    }
    */
}
