using System.Collections;
using UnityEngine;

public class powerPill : MonoBehaviour
{
    //GameObject[] enemies;
    static soundsManager au;

    void Start()
    {
        //enemies = GameObject.FindGameObjectsWithTag("enemy");
        au = GameObject.Find("GameManager").GetComponent<soundsManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        disabledEnemy.enemiesToArray();

        if (collision.CompareTag("Player"))
        {
            au.playSoundWithStop(3);
            au.playSound(2);
            //if (collision.gameObject != null && gameObject != null) // Kontroller ekleyin
            ///{
            foreach (GameObject enemy in disabledEnemy.enemies)
            {
                //if (enemy != null) // Kontrol ekleyin
               //enemy.GetComponent<npcFollow>().isGhost = true;
                enemy.GetComponent<npcFollow>().isGhost = true;
                enemy.GetComponent<npcFollow>().ghostState = true;
            }

            //if (npcFollow.ghostTime < 10)
                npcFollow.ghostTime += 5;

            print("before ghostTime:" + npcFollow.ghostTime);
            //disabledEnemy.enemies[0].GetComponent<npcFollow>().reduceGhostTime();
            //print("after1 ghostTime:" + npcFollow.ghostTime);
            gameObject.SetActive(false);
            //}
        }
    }
}
