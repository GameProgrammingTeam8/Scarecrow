using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}