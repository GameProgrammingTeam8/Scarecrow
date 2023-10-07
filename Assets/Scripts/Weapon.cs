using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        HP hp = other.GetComponent<HP>();

        if (hp != null)
        {
            hp.amount -= damage;
        }
    }
}
