using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Vector2 movementValue;
    public bool isAttack = false;
    private bool isSkill = false;
    private float lookValue;
    private Rigidbody rb;
    ParticleSystem ps;
    Animator anim;
    AudioSource aud;
    public AudioClip attackSFX;
    public AudioClip skillSFX;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        aud = GetComponent<AudioSource>();
    }

    public void OnMove(InputValue value)
    {
        if (GetComponent<HP>().amount > 0)
        {
            movementValue = value.Get<Vector2>() * speed;
            anim.SetBool("isMove", movementValue != Vector2.zero);
        }
        else
        {
            movementValue = Vector2.zero;
        }
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x * rotationSpeed;
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            aud.clip = attackSFX;
            isAttack = true;
            anim.SetTrigger("Attack");
            aud.PlayOneShot(aud.clip);
        }
    }

    public void OnSkill(InputValue value)
    {
        if (value.isPressed && isSkill == false)
        {
            aud.clip = skillSFX;
            isAttack = true;
            isSkill = true;
            anim.SetTrigger("Skill");
            aud.PlayOneShot(aud.clip);
            StartCoroutine(StartCooltime());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime);
        rb.AddRelativeTorque(0, lookValue * Time.deltaTime, 0);
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
        if(other.CompareTag("Enemy") && isAttack==false)
        {
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(KnockBack(reactVec));
        }
    }

    IEnumerator KnockBack(Vector3 reactVec)
    {
        ps.Play();
        reactVec = reactVec.normalized;
        rb.AddForce(reactVec * 30, ForceMode.Impulse);
        speed -= 2000;

        yield return new WaitForSeconds(2);
        ps.Stop();
        speed += 2000;
    }

    IEnumerator StartCooltime()
    {
        
        yield return new WaitForSecondsRealtime(1);

        yield return new WaitForSecondsRealtime(1);

        yield return new WaitForSecondsRealtime(1);

        isSkill = false;
    }

}
