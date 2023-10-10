using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public float amount;
    public UnityEvent onDeath;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(DeathCheck());
    }

    IEnumerator DeathCheck()
    {
        while (amount > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (CompareTag("Player"))
        {
            anim.SetTrigger("Die");
            yield return new WaitForSecondsRealtime(5);

            transform.position = new Vector3(0, 0, 0);
            SceneManager.LoadScene("ResultMode");
            amount = 1000;
            anim.SetBool("isResult", true);
        }
        else if (CompareTag("Enemy"))
        {
            Enemy enemy = GetComponent<Enemy>();
            enemy.enabled = false;
            onDeath.Invoke();
            Destroy(gameObject, 3);
        }
    }
}
