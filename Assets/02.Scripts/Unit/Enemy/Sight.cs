using UnityEngine;

public class Sight : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _angle;
    [SerializeField] private LayerMask _objectsLayers;
    [SerializeField] private LayerMask _obstaclesLayers;
    
    public Collider DetectedObject { get; private set; }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distance, _objectsLayers);
        DetectedObject = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            Vector3 directionToController = Vector3.Normalize(collider.bounds.center - transform.position);
            float angleToCollider = Vector3.Angle(transform.forward, directionToController);
            if (angleToCollider < _angle)
            {
                if (!Physics.Linecast(transform.position, collider.bounds.center, _obstaclesLayers))
                {
                    DetectedObject = collider;
                    break;
                }
            }
        }
    }
}