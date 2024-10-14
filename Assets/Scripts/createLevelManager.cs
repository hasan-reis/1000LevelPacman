using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createLevelManager : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject mainEnemyObject;
    public static GameObject mainEnemy;

    void Awake()
    {
        if (GameObject.Find("levelManager(Clone)") == null)
        {
            GameObject lvlManager=Instantiate(levelManager);
            mainEnemy = Instantiate(mainEnemyObject, new Vector3(0.0423642844f, -0.0615963638f, 0.00817680731f),Quaternion.identity);
            mainEnemy.transform.parent= lvlManager.transform;
        }
    }

    void Update()
    {
        
    }
}
