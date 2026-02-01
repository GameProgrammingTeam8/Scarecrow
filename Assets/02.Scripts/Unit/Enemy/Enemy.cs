using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HP))]
public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;
    private EnemyFSM _ai;
    private HP _hp;
    private ParticleSystem _hitEffect;
    private GameObject _target;
    private HP _targetHP;

    public float Damage;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _ai = GetComponentInChildren<EnemyFSM>();
        _hp = GetComponent<HP>();
        _hitEffect = GetComponent<ParticleSystem>();
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

    private void FixedUpdate()
    {
        // Freeze Velocity
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    // Hit Detection
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            StartCoroutine(KnockBack(new Vector3(
                transform.position.x - other.transform.position.x,
                0,
                transform.position.z - other.transform.position.z
            )));
        }
        else if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(Damage);
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

    // Knockback 반응 처리
    private IEnumerator KnockBack(Vector3 reactVec)
    {
        _hitEffect.Play();
        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        _rigidbody.AddForce(reactVec * 20, ForceMode.Impulse);

        yield return new WaitForSeconds(0.3f);
        _hitEffect.Stop();
        _rigidbody.linearVelocity = Vector3.zero;
    }
}