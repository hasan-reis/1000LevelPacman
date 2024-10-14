using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class disabledEnemy : MonoBehaviour
{
    public static Transform mainEnemyObject;
    public static List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        enemiesToArray();
    }

    void Update()
    {
    }

    public static void enemiesToArray()
    {
        enemies.Clear();

        string mainEnemyName;
        if (GameManager.levelNumber % 2 != 0)
            mainEnemyName = "4enemies(Clone)";
        else
            mainEnemyName = "4enemies2(Clone)";
        mainEnemyObject = GameObject.Find(mainEnemyName).transform;

        for (int i = 0; i < mainEnemyObject.childCount; i++)
        {
            enemies.Add(mainEnemyObject.GetChild(i).gameObject);
        }
    }

    void newEnemyToArray(GameObject enemy)
    {
        enemies.Add(enemy);
        enemy.transform.parent = mainEnemyObject;
    }

    static void locateEnemy()
    {
        Vector2 blinky1Pos;
        Vector2 blinky2Pos;
        Vector2 blinky3Pos;
        Vector2 blinky4Pos;

        if (GameManager.levelNumber % 2 != 0)
        {
            blinky1Pos = new Vector2(-0.282364279f, 0.358596355f);
            blinky2Pos = new Vector2(-0.662364304f, 0.358596355f);
            blinky3Pos = new Vector2(0.505999982f, 0.358596355f);
            blinky4Pos = new Vector2(0.115999997f, 0.358596355f);
        }
        else
        {
            blinky1Pos = new Vector2(3.07999992f, 0.550000012f);
            blinky2Pos = new Vector2(0.419999987f, 0.74000001f);
            blinky3Pos = new Vector2(0.400000006f, 0.170000002f);
            blinky4Pos = new Vector2(3.08999991f, 0.100000001f);
        }

        foreach (GameObject enmy in enemies)
            {
                switch(enmy.name)
                {
                    case "blinky_4":
                    enmy.transform.position = blinky1Pos;
                        break;
                case "blinky_4 (1)":
                    enmy.transform.position = blinky2Pos;
                    break;
                case "blinky_4 (2)":
                    enmy.transform.position = blinky3Pos;
                    break;
                case "blinky_4 (3)":
                    enmy.transform.position = blinky4Pos;
                    break;
                default:
                    break;
            }

            }
    }

    void enemiesPassive()
    {
        enemies.Clear();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<npcFollow>().enabled && enemy.GetComponent<NavMeshAgent>().enabled)
            {
                enemy.GetComponent<npcFollow>().enabled = false;
                enemy.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        /* chatgpt hata cozum onerisi:
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                npcFollow followComponent = enemy.GetComponent<npcFollow>();
                if (followComponent != null && followComponent.enabled)
                {
                    followComponent.enabled = false;
                }
                NavMeshAgent navAgentComponent = enemy.GetComponent<NavMeshAgent>();
                if (navAgentComponent != null && navAgentComponent.enabled)
                {
                    navAgentComponent.enabled = false;
                }
            }
        }*/
    }

   //public int nextLjevel(int sceneIndex)
   //{
   //    food.score = 0;
   //    food.eatenFood = 0;
   //    return (sceneIndex == 1) ? 2 : 1;
   //}
}
