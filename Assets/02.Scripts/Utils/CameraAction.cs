using UnityEngine;

public class CameraAction : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _offsetX = 0;
    [SerializeField] private float _offsetY = 10;
    [SerializeField] private float _offsetZ = -3;

    private void Update()
    {
        Vector3 updatedPosition = new(
            _target.transform.position.x + _offsetX,
            _target.transform.position.y + _offsetY,
            _target.transform.position.z + _offsetZ
        );

        transform.position = updatedPosition;
    }
}