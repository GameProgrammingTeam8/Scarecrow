using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float damage;
    public Transform target;
    Rigidbody rigid;
    public GameObject player;
    public HP playerHP;
    ParticleSystem ps;

    public NavMeshAgent nav;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        ps = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player");
        playerHP = GameObject.Find("Player").GetComponent<HP>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        EnemyManager.instance.AddEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.enabled == true)
        {
            nav.SetDestination(target.position);
        }
        if(playerHP.amount <= 0)
        {
            nav.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    public void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(KnockBack(reactVec));
        }
    }

    IEnumerator KnockBack(Vector3 reactVec)
    {
        ps.Play();
        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * 10, ForceMode.Impulse);

        yield return new WaitForSeconds(0.3f);
        ps.Stop();
        rigid.velocity = Vector3.zero;
    }

}
