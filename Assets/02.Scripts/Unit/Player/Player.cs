using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(HitReaction))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private bool _inTutorialMode = false;
    [SerializeField] private float _moveSpeed = 4;    
    [SerializeField] private AudioClip _attackSFX;
    [SerializeField] private AudioClip _skillSFX;
    [SerializeField] private AudioClip _defendSFX;

    private bool _isRush = false;
    private bool _isSlashRCooldown = false;
    private bool _isShieldRushCooldown = false;
    private Vector2 _movePosition;
    private Animator _animator;
    private AudioSource _audioSource;
    private HP _hp;
    private HitReaction _hitReaction;
    private RectTransform _hpLine;
    private GameObject _basicAttackDeactivationUI;
    private GameObject _slashRDeactivationUI;
    private GameObject _shieldRushDeactivationUI;
    private ParticleSystem _shieldRushEffect;
    private TextMeshProUGUI _attackCooldownUI;
    private TextMeshProUGUI _slashRCooldownUI;
    private TextMeshProUGUI _shieldRushCooldownUI;

    public bool IsAttacking { get; private set; }
    public bool IsSkillUsing { get; private set; }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _hp = GetComponent<HP>();
        _hitReaction = GetComponent<HitReaction>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _shieldRushEffect = GameObject.Find("SkillEffect").GetComponent<ParticleSystem>();
        _shieldRushEffect.gameObject.SetActive(false);
        
        _hpLine = GameObject.Find("HP Line").GetComponent<RectTransform>();
        _attackCooldownUI = GameObject.Find("AttackCooldownUI").GetComponent<TextMeshProUGUI>();
        _slashRCooldownUI = GameObject.Find("SlashRCooldownUI").GetComponent<TextMeshProUGUI>();
        _shieldRushCooldownUI = GameObject.Find("CoolNumS").GetComponent<TextMeshProUGUI>();

        _basicAttackDeactivationUI = GameObject.Find("HideAttack");
        _slashRDeactivationUI = GameObject.Find("HideSkill1");
        _shieldRushDeactivationUI = GameObject.Find("HideSkill3");

        _basicAttackDeactivationUI.SetActive(false);
        _slashRDeactivationUI.SetActive(false);
        _shieldRushDeactivationUI.SetActive(false);
    }

    private void Update()
    {
        transform.position += new Vector3(
            _movePosition.x * Time.deltaTime,
            0,
            _movePosition.y * Time.deltaTime
        );
    }

    // Hit Detection
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") ||
            other.CompareTag("Bullet"))
        {
            if (IsAttacking || IsSkillUsing || _isRush) return;

            Vector3 direction = new(
                transform.position.x - other.transform.position.x,
                0,
                transform.position.z - other.transform.position.z
            );

            _hitReaction.Play(direction);
        }
    }

    // Damage 처리
    public void TakeDamage(float damage = 5000)
    {
        if (_inTutorialMode) return;

        _hp.Decrease(damage);
        if (_hp.Value <= 0) StartCoroutine(GoToResultMode());

        _hpLine.localScale = new Vector3(_hp.Value / _hp.MaxValue, 1, 1);
    }

    // 승리 시 애니메이션 재생
    public void Victory()
    {
        _animator.SetTrigger("Victory");
    }

    // 사망 시 Result Mode로 이동 처리
    private IEnumerator GoToResultMode()
    {
        _animator.SetTrigger("Die");
        yield return new WaitForSecondsRealtime(5);

        SceneManager.LoadScene("ResultMode");
        transform.position = Vector3.zero;
        _hp.Increase(_hp.MaxValue);
        _animator.SetBool("isResult", true);
    }

    public void OnMove(InputValue value)
    {
        _movePosition = Vector2.zero;
        if (_hp.Value <= 0) return;
        
        _movePosition = value.Get<Vector2>() * _moveSpeed;
        _animator.SetBool("isMove", _movePosition != Vector2.zero);
        
        if (_movePosition == Vector2.zero) return;
        
        transform.rotation = Quaternion.Euler(
            0f,
            Mathf.Atan2(_movePosition.x, _movePosition.y) * Mathf.Rad2Deg,
            0f
        );
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed &&
            !IsAttacking &&
            !IsSkillUsing &&
            _hp.Value > 0)
        {
            StartCoroutine(Attack());
        }
    }

    public void OnSlashR(InputValue value)
    {
        if (value.isPressed &&
            !IsAttacking &&
            !IsSkillUsing &&
            !_isSlashRCooldown &&
            _hp.Value > 0)
        {
            StartCoroutine(SlashR());
        }
    }

    public void OnShieldRush(InputValue value)
    {
        if (value.isPressed &&
            !IsAttacking &&
            !IsSkillUsing &&
            !_isShieldRushCooldown &&
            _hp.Value > 0)
        {
            StartCoroutine(ShieldRush());
        }
    }

    private IEnumerator Attack()
    {
        IsAttacking = true;
        _animator.SetTrigger("Attack");
        _audioSource.PlayOneShot(_attackSFX);
        _basicAttackDeactivationUI.SetActive(true);
        _attackCooldownUI.SetText("");
        yield return new WaitForSeconds(0.6f);
        _basicAttackDeactivationUI.SetActive(false);
        IsAttacking = false;
    }

    private IEnumerator SlashR()
    {
        IsSkillUsing = true;
        _animator.SetTrigger("Skill");
        _audioSource.PlayOneShot(_skillSFX);
        _audioSource.PlayDelayed(1.6f);
        _isSlashRCooldown = true;
        _slashRDeactivationUI.SetActive(true);

        for (int i = 9; i > 0; i--)
        {
            _slashRCooldownUI.SetText(i.ToString());
            yield return new WaitForSecondsRealtime(1);
            if (i == 7) IsSkillUsing = false;
        }
        
        _slashRDeactivationUI.SetActive(false);
        _isSlashRCooldown = false;
    }

    private IEnumerator ShieldRush()
    {
        IsSkillUsing = true;
        _isRush = true;
        _shieldRushEffect.gameObject.SetActive(true);
        _shieldRushEffect.Play();
        _animator.SetTrigger("ShieldRush");
        _moveSpeed += 3;
        _isShieldRushCooldown = true;
        _audioSource.PlayOneShot(_defendSFX);
        _shieldRushDeactivationUI.SetActive(true);

        for (int i = 7; i > 0; i--)
        {
            _shieldRushCooldownUI.SetText(i.ToString());

            if (i == 6)
            {
                yield return new WaitForSecondsRealtime(0.5f);
                _moveSpeed -= 3;
                _shieldRushEffect.Stop();
                _shieldRushEffect.gameObject.SetActive(false);
                IsSkillUsing = false;
                _isRush = false;
                yield return new WaitForSecondsRealtime(0.5f);
            }
            else yield return new WaitForSecondsRealtime(1);
        }

        _shieldRushDeactivationUI.SetActive(false);
        _isShieldRushCooldown = false;
    }
}