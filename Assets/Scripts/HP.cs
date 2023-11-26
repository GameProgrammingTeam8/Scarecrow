using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public float amount;
    public bool isTutorial = false;
    public int hitCount = 0;

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

            SceneManager.LoadScene("ResultMode");
            transform.position = new Vector3(0, 0, 0);
            amount = 2000;
            anim.SetBool("isResult", true);
        }
    }
}
