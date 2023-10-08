using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class HP : MonoBehaviour
{
    public float amount;
    public UnityEvent onDeath;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (amount <= 0)
        {
            if (CompareTag("Player"))
            {
                anim.SetTrigger("Die");
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 5)
                {
                    amount = 1000;
                    anim.SetBool("isResult", true);
                    SceneManager.LoadScene("ResultMode");
                    transform.position = new Vector3(0, 0, 0);
                }
            }
            else if (CompareTag("Enemy"))
            {
                Enemy enemy = GetComponent<Enemy>();
                enemy.enabled = false;
                onDeath.Invoke();
                Destroy(gameObject,3);
            }
        }
    }
}
