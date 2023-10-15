using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float startTime;
    public float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startTime, spawnRate);
    }
    void Spawn()
    {
        Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
    }
}
