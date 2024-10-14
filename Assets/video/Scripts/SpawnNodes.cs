using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodes : MonoBehaviour
{
    int numToSpawn = 28;
    public float currentSpawnOffset;
    public float spawnOffset = 0.3f;

    void Start()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
