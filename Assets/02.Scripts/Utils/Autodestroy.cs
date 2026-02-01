using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    [SerializeField] private float delay = 7;
    
    private void Start()
    {
        Destroy(gameObject, delay);
    }
}