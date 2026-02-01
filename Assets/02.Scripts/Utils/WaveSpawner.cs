using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float startTime;
    [SerializeField] private float spawnRate;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), startTime, spawnRate);
    }

    private void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}