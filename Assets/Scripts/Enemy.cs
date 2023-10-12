using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float damage;
    public Transform target;
    Rigidbody rigid;
    Player p;
    ParticleSystem ps;

    public NavMeshAgent nav;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        p = target.GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        EnemyManager.instance.AddEnemy(this);
    }

    private void OnDestroy()
    {
        EnemyManager.instance.RemoveEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.enabled == true)
        {
            nav.SetDestination(target.position);
        }
        if(p.GetComponent<HP>().amount <= 0)
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
        HP hp = other.GetComponent<HP>();

        if (other.tag == "Player" && p.isAttack == false && p.isSkill == false)
        {
            if (hp != null)
            {
                hp.amount -= damage;
                p.hpLine.localScale = new Vector3(hp.amount / p.maxHP, 1, 1);
            }
        }
        else if(other.tag == "Weapon")
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
