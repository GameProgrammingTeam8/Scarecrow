using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HP))]
[RequireComponent(typeof(HitReaction))]
public class Enemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent _agent;
    private EnemyFSM _ai;
    private HP _hp;
    private HitReaction _hitReaction;
    private GameObject _target;
    private HP _targetHP;

    public float Damage;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _ai = GetComponentInChildren<EnemyFSM>();
        _hp = GetComponent<HP>();
        _hitReaction = GetComponent<HitReaction>();
        _target = GameObject.Find("Player");
        _targetHP = _target.GetComponent<HP>();
        
        EnemyManager.instance.AddEnemy(this);
    }

    private void Update()
    {
        if (_targetHP == null) return;
        if (_agent.enabled) _agent.SetDestination(_target.transform.position);
        if (_targetHP.Value <= 0) _agent.enabled = false;
    }

    // Hit Detection
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            Vector3 direction = new(
                transform.position.x - other.transform.position.x,
                0,
                transform.position.z - other.transform.position.z
            );

            _hitReaction.Play(direction);
        }
        else if(other.CompareTag("Player"))
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(Damage);
            }
        }
    }

    // Damage 처리
    public void TakeDamage(float damage)
    {
        _hp.Decrease(damage);
        if (_hp.Value <= 0)
        {
            if (_ai.CurrentState == EnemyState.Die) return;

            _ai.CurrentState = EnemyState.Die;
            EnemyManager.instance.RemoveEnemy(this);
            enabled = false;

            Destroy(gameObject, 0.1f);
        }
    }
}