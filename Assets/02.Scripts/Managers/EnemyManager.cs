using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<Enemy> enemies;
    public int genEnemy = 0;
    public int destroyedEnemy = 0;
    
    public UnityEvent OnChanged;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning(
                "Duplicated EnemyManager, ignoring this one",
                gameObject
            );
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        genEnemy += 1;
        OnChanged.Invoke();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        destroyedEnemy += 1;
        OnChanged.Invoke();
    }
}