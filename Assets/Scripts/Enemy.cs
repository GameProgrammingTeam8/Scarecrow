using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;

    void Start()
    {
        EnemyManager.instance.AddEnemy(this);
    }

    private void OnDestroy()
    {
        EnemyManager.instance.RemoveEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HP hp = other.GetComponent<HP>();

        if (other.tag == "Player")
        {
            if (hp != null)
            {
                hp.amount -= damage;
            }
        }
    }
}
