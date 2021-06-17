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
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnSpike), 1f, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke(nameof(SpawnSpike));
        enabled = false;
    }
}
