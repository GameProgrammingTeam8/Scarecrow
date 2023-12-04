using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float maxHP;
    public RectTransform hpBar;
    public RectTransform hpLine;
    public GameObject HideAttack;
    public GameObject HideSkill1;
    public GameObject HideSkill2;
    public GameObject HideSkill3;
    public GameObject SkillEffect;
    public TextMeshProUGUI CoolNum1;
    public TextMeshProUGUI CoolNum2;
    public TextMeshProUGUI CoolNum3;
    public TextMeshProUGUI CoolNumS;
    private Vector2 movementValue;
    public bool isAttack = false;
    public bool isSkill = false;
    public bool isRush = false;
    private bool isCoolTime = false;
    //private bool isCoolTimeD = false;
    private bool isCoolTimeS = false;
    private float lookValue;
    private float lookX;
    private float lookZ;
    private Rigidbody rb;

    private ParticleSystem ps;
    private ParticleSystem effect;
    Animator anim;
    AudioSource aud;

    public AudioClip attackSFX;
    public AudioClip skillSFX;
    public AudioClip defendSFX;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        hpBar = GameObject.Find("HPBar").GetComponent<RectTransform>();
        hpLine = GameObject.Find("HP Line").GetComponent<RectTransform>();
        HideAttack = GameObject.Find("HideAttack");
        HideSkill1 = GameObject.Find("HideSkill1");
        //HideSkill2 = GameObject.Find("HideSkill2");
        HideSkill3 = GameObject.Find("HideSkill3");
        SkillEffect = GameObject.Find("SkillEffect");
        CoolNum1 = GameObject.Find("CoolNum1").GetComponent<TextMeshProUGUI>();
        CoolNum2 = GameObject.Find("CoolNum2").GetComponent<TextMeshProUGUI>();
        //CoolNum3 = GameObject.Find("CoolNum3").GetComponent<TextMeshProUGUI>();
        CoolNumS = GameObject.Find("CoolNumS").GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        effect = SkillEffect.GetComponent<ParticleSystem>();
        aud = GetComponent<AudioSource>();
    }

    private void Start()
    {
        HideSkill1.SetActive(false);
        HideAttack.SetActive(false);
        //HideSkill2.SetActive(false);
        HideSkill3.SetActive(false);
        maxHP = GetComponent<HP>().amount;
    }

    public void OnMove(InputValue value)
    {
        if (GetComponent<HP>().amount > 0)
        {
            movementValue = value.Get<Vector2>() * speed;
            anim.SetBool("isMove", movementValue != Vector2.zero);
            if (movementValue != Vector2.zero)
            {
                transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(movementValue.x, movementValue.y) * Mathf.Rad2Deg, 0f);
            }
        }
        else
        {
            movementValue = Vector2.zero;
        }
    }

    /*public void OnLook(InputValue value)
    {
        lookX = value.Get<Vector2>().x;
        lookZ = value.Get<Vector2>().y;
        if(GetComponent<HP>().amount > 0)
            lookValue = value.Get<Vector2>().x * rotationSpeed;
    }*/

    public void OnSlash(InputValue value)
    {
        if (value.isPressed && isAttack == false && isSkill == false && GetComponent<HP>().amount > 0)
        {
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack()
    {
        aud.clip = attackSFX;
        isAttack = true;
        anim.SetTrigger("Attack");
        aud.PlayOneShot(aud.clip);
        HideAttack.SetActive(true);
        CoolNum1.SetText("");
        yield return new WaitForSeconds(0.6f);
        HideAttack.SetActive(false);
        isAttack = false;
    }

    public void OnRSlash(InputValue value)
    {
        if (value.isPressed && isAttack == false && isSkill == false && isCoolTime == false && GetComponent<HP>().amount > 0)
        {
            StartCoroutine(Skill1());
        }
    }

    IEnumerator Skill1()
    {
        aud.clip = skillSFX;
        isSkill = true;
        anim.SetTrigger("Skill");
        aud.PlayOneShot(aud.clip);
        aud.PlayDelayed(1.6f);
        isCoolTime = true;
        HideSkill1.SetActive(true);
        CoolNum2.SetText("9");
        yield return new WaitForSecondsRealtime(1);
        CoolNum2.SetText("8");
        yield return new WaitForSecondsRealtime(1);
        CoolNum2.SetText("7");
        yield return new WaitForSecondsRealtime(1);
        isSkill = false;
        CoolNum2.SetText("6");
        yield return new WaitForSecondsRealtime(1);
        CoolNum2.SetText("5");
        yield return new WaitForSecondsRealtime(1);
        CoolNum2.SetText("4");
        yield return new WaitForSecondsRealtime(1);
        CoolNum2.SetText("3");
        yield return new WaitForSecondsRealtime(1);
        CoolNum2.SetText("2");
        yield return new WaitForSecondsRealtime(1);
        CoolNum2.SetText("1");
        yield return new WaitForSecondsRealtime(1);
        HideSkill1.SetActive(false);
        isCoolTime = false;
    }

    /*public void OnDefend(InputValue value)
    {
        if (value.isPressed && isAttack == false && isSkill == false && isCoolTimeD == false && GetComponent<HP>().amount > 0)
        {
            StartCoroutine(Skill2());
        }
    }

    IEnumerator Skill2()
    {
        aud.clip = defendSFX;
        isSkill = true;
        anim.SetTrigger("Defend");
        rb.AddForce(lookX * 300 * Time.deltaTime, 0, lookZ * 300 * Time.deltaTime, ForceMode.VelocityChange);
        aud.PlayOneShot(aud.clip);
        isCoolTimeD = true;
        HideSkill2.SetActive(true);
        CoolNum3.SetText("5");
        yield return new WaitForSecondsRealtime(1);
        isSkill = false;
        CoolNum3.SetText("4");
        yield return new WaitForSecondsRealtime(1);
        CoolNum3.SetText("3");
        yield return new WaitForSecondsRealtime(1);
        CoolNum3.SetText("2");
        yield return new WaitForSecondsRealtime(1);
        CoolNum3.SetText("1");
        yield return new WaitForSecondsRealtime(1);
        HideSkill2.SetActive(false);
        isCoolTimeD = false;
    }*/

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime);
        /*if (GetComponent<HP>().amount > 0)
        {
            Vector3 dir = new Vector3(js.Horizontal, 0, js.Vertical);
            dir.Normalize();
            transform.position += dir * speed * Time.deltaTime;
            anim.SetBool("isMove", dir != Vector3.zero);
        }*/
        //rb.AddRelativeForce(movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime);
        //rb.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        FreezeRotation();
    }

    public void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isAttack == false && isSkill == false && isRush == false)
        {
            HP hp = GetComponent<HP>();
            if (hp != null && hp.isTutorial == false)
            {
                hp.amount -= other.GetComponent<Enemy>().damage;
                if (hp.amount < 0)
                {
                    hp.amount = 0;
                }
                hpLine.localScale = new Vector3(hp.amount / maxHP, 1, 1);
            }
            Vector3 reactVec = new Vector3(transform.position.x - other.transform.position.x, 0, transform.position.z - other.transform.position.z);
            StartCoroutine(KnockBack(reactVec));
        }
        if (other.CompareTag("Bullet") && isRush == false)
        {
            Vector3 reactVec = new Vector3(transform.position.x - other.transform.position.x, 0, transform.position.z - other.transform.position.z);
            StartCoroutine(KnockBack(reactVec));
        }
    }

    IEnumerator KnockBack(Vector3 reactVec)
    {
        ps.Play();
        effect.Stop();
        anim.SetTrigger("GetHit");
        reactVec = reactVec.normalized;
        rb.AddForce(reactVec * 20, ForceMode.Impulse);

        yield return new WaitForSeconds(1);
        ps.Stop();
        rb.velocity = Vector3.zero;
    }

    public void Victory()
    {
        anim.SetTrigger("Victory");
    }

    public void OnShieldRush(InputValue value)
    {
        if (value.isPressed && isAttack == false && isSkill == false && isCoolTimeS == false && GetComponent<HP>().amount > 0)
        {
            StartCoroutine(Skill3());
        }
    }

    IEnumerator Skill3()
    {
        aud.clip = defendSFX;
        isSkill = true;
        isRush = true;
        effect.Play();
        anim.SetTrigger("ShieldRush");
        speed += 3;
        isCoolTimeS = true;
        aud.PlayOneShot(aud.clip);
        HideSkill3.SetActive(true);
        CoolNumS.SetText("7");
        yield return new WaitForSecondsRealtime(1);
        CoolNumS.SetText("6");
        yield return new WaitForSecondsRealtime(0.5f);
        speed -= 3;
        effect.Stop();
        isSkill = false;
        isRush = false;
        yield return new WaitForSecondsRealtime(0.5f);
        CoolNumS.SetText("5");
        yield return new WaitForSecondsRealtime(1);
        CoolNumS.SetText("4");
        yield return new WaitForSecondsRealtime(1);
        CoolNumS.SetText("3");
        yield return new WaitForSecondsRealtime(1);
        CoolNumS.SetText("2");
        yield return new WaitForSecondsRealtime(1);
        CoolNumS.SetText("1");
        yield return new WaitForSecondsRealtime(1);
        HideSkill3.SetActive(false);
        isCoolTimeS = false;
    }
}
