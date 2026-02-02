using UnityEngine;

public class ScareCrow : MonoBehaviour, IDamageable
{
    private Animator _animator;
    private HP _hp;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _hp = GetComponent<HP>();
        ScareCrowManager.instance.AddScareCrow(this);
    }

    public void TakeDamage(float damage)
    {
        _hp.Decrease(damage);
        if (_hp.Value <= 0)
        {
            if (!enabled) return;

            _animator.SetTrigger("Attacked");
            ScareCrowManager.instance.RemoveScareCrow(this);
            Destroy(gameObject, 0.1f);
            enabled = false;
        }
    }
}