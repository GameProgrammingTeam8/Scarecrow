using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform Target;
    public float HideDistance;

    private void Update()
    {
        if(Target == null)
        {
            foreach (Transform child in transform)
            {
                SetChildActive(false);
            }
            enabled=false;
            return;
        }
        var dir=Target.position-transform.position;
        if(dir.magnitude<HideDistance)
        {
            foreach(Transform child in transform)
            {
                SetChildActive(false);
            }
        }
        else
        {
            SetChildActive(true);
            var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
        }
    }

    private void SetChildActive(bool value)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}