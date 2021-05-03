using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    private float spawnRate = 1f;
    public float xPositionLimit;
    void Start()
    {
       StartSpawning();
    }
    void SpawnSpike()
    {
        
        float randomX = Random.Range(-xPositionLimit, xPositionLimit);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        GameObject tempEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnSpike", 1f, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnSpike");
        enabled = false;
    }
}
