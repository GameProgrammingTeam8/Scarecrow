using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform Target;
    public float HideDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    void SetChildActive(bool value)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
}
