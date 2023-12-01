using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { ChasePlayer, AttackPlayer }
    public EnemyState currentState;
    public Sight sightSensor;
    public float playerAttackDistance;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;
    public HP life;
    public HP playerLife;
    public GameObject player;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerLife = GameObject.FindWithTag("Player").GetComponent<HP>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.ChasePlayer)
        {
            ChasePlayer();
        }
        else if(currentState == EnemyState.AttackPlayer)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.isStopped = false;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer <= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }
    void AttackPlayer()
    {
        if (agent.isActiveAndEnabled && sightSensor.detectedObject!=null)
        {
            agent.isStopped = true;
            LookTo(sightSensor.detectedObject.transform.position);
            Shoot();
            float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
            if (distanceToPlayer > playerAttackDistance * 1.1f)
            {
                currentState = EnemyState.ChasePlayer;
            }
        }
        else if (agent.isActiveAndEnabled && sightSensor.detectedObject == null)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
