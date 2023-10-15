using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<Enemy> enemies;
    public int genEnemy = 0;
    public int destroyedEnemy = 0;
    public UnityEvent onChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated EnemyManager, ignoring this one", gameObject);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        genEnemy += 1;
        onChanged.Invoke();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        destroyedEnemy += 1;
        onChanged.Invoke();
    }
}
