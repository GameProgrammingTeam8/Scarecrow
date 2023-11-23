using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public GameObject target;
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private void Update()
    {
        Vector3 FixedPos = new Vector3(target.transform.position.x + offsetX,
            target.transform.position.y + offsetY,
            target.transform.position.z + offsetZ);
        transform.position = FixedPos;
    }
    /*private Transform tr;

    public float dist = 10.0f;
    public float height = 10.0f;
    public float smoothRotate = 5.0f;
    public float CameraSpeed = 10.0f;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Camera that rotates to follow the direction the player is looking.
    private void LateUpdate()
    {
        float curYAngle = Mathf.LerpAngle(tr.eulerAngles.y, target.transform.eulerAngles.y, smoothRotate * Time.deltaTime);
        Quaternion rot = Quaternion.Euler(0, curYAngle, 0);
        tr.position = target.transform.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
        //tr.LookAt(target.transform);
    }*/
}
