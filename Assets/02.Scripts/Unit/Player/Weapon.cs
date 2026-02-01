using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _damage = 100;
    [SerializeField] private AudioClip _sliceSFX;
    private AudioSource _audioSource;
    private Player _player;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_player.IsAttacking && !_player.IsSkillUsing) return;
        
        _audioSource.PlayOneShot(_sliceSFX);
        
        if (other.CompareTag("Enemy"))
        {
            var target = other.GetComponent<Enemy>();
            target.TakeDamage(_damage);
        }
        else if (other.CompareTag("ScareCrow"))
        {
            var target = other.GetComponent<ScareCrow>();
            target.TakeDamage(_damage);
        }
    }
}