using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.CompareTag("Player"))
        {
            var target = other.GetComponent<Player>();
            target.TakeDamage(_damage);
        }
    }
}