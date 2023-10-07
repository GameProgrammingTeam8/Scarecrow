using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HP : MonoBehaviour
{
    public float amount;
    public float timePassed = 0;
    public UnityEvent onDeath;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (amount <= 0)
        {
            if(CompareTag("Player"))
            {
                anim.SetTrigger("Die");
                timePassed += Time.deltaTime;
                if (timePassed >= 3)
                {
                    amount += 1000;
                    anim.SetTrigger("replay");
                }
            }
            else
            {
                onDeath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
