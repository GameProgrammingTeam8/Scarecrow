using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float damage;
    public Transform target;

    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        EnemyManager.instance.AddEnemy(this);
    }

    private void OnDestroy()
    {
        EnemyManager.instance.RemoveEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);
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
