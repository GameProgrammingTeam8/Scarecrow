using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Sight))]
public class EnemyFSM : MonoBehaviour
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _fireRate = 2;
    [SerializeField] private GameObject _bulletPrefab;
    private float _lastShootTime;
    private NavMeshAgent _agent;
    private Sight _sight;
    private GameObject _target;

    public EnemyState CurrentState { get; set; } = EnemyState.Chase;
    
    private void Start()
    {
        _agent = GetComponentInParent<NavMeshAgent>();
        _sight = GetComponent<Sight>();
        _target = GameObject.Find("Player");
    }

    private void Update()
    {
        if (CurrentState == EnemyState.Chase) Chase();
        else if (CurrentState == EnemyState.Attack) Attack();
    }

    // 추격 상태 처리
    private void Chase()
    {
        if (_agent.isActiveAndEnabled) _agent.isStopped = false;

        float distanceToPlayer = Vector3.Distance(
            transform.position,
            _target.transform.position
        );

        if(distanceToPlayer <= _attackDistance)
        {
            CurrentState = EnemyState.Attack;
        }
    }

    // 공격 상태 처리
    private void Attack()
    {
        if (!_agent.isActiveAndEnabled) return;

        if (_sight.DetectedObject == null)
        {
            CurrentState = EnemyState.Chase;
            return;
        }
        
        _agent.isStopped = true;

        LookTo(_sight.DetectedObject.transform.position);
        Shoot();

        float distanceToPlayer = Vector3.Distance(
            transform.position,
            _sight.DetectedObject.transform.position
        );

        if (distanceToPlayer > _attackDistance * 1.1f)
        {
            CurrentState = EnemyState.Chase;
        }
    }

    private void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(
            targetPosition - transform.parent.position
        );

        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }

    private void Shoot()
    {
        var timeSinceLastShoot = Time.time - _lastShootTime;
        if (timeSinceLastShoot > _fireRate)
        {
            _lastShootTime = Time.time;
            Instantiate(
                _bulletPrefab,
                transform.position,
                transform.rotation
            );
        }
    }
}