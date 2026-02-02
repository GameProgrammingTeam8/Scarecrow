using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ParticleSystem))]
public class HitReaction : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 20f;
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private Vector3 _extraDirection = Vector3.zero;

    private static readonly int GetHitHash = Animator.StringToHash("GetHit");
    
    private bool _hasGetHitTrigger;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private ParticleSystem _hitEffect;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _hitEffect = GetComponent<ParticleSystem>();

        if (_animator != null)
        {
            foreach (var param in _animator.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Trigger &&
                    param.nameHash == GetHitHash)
                {
                    _hasGetHitTrigger = true;
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // Freeze Velocity
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void Play(Vector3 direction)
    {
        StartCoroutine(ReactionRoutine(direction));
    }

    private IEnumerator ReactionRoutine(Vector3 direction)
    {
        _hitEffect.Play();
        if (_hasGetHitTrigger) _animator.SetTrigger(GetHitHash);

        Vector3 forceDirection = (direction.normalized + _extraDirection).normalized;
        
        _rigidbody.AddForce(
            forceDirection * _knockbackForce,
            ForceMode.Impulse
        );

        yield return new WaitForSecondsRealtime(_duration);

        _hitEffect.Stop();
        _rigidbody.linearVelocity = Vector3.zero;
    }
}