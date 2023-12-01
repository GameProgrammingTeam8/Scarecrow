using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.CompareTag("Player"))
        {
            HP life = other.GetComponent<HP>();
            Player p = other.GetComponent<Player>();

            if (life != null)
            {
                life.amount -= damage;
            }
            if (life.amount < 0)
            {
                life.amount = 0;
            }
            p.hpLine.localScale = new Vector3(life.amount / p.maxHP, 1, 1);
        }
    }
}
