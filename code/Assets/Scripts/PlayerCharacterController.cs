using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance;

    public Camera playerCamera;
    public float gravityDownForce = 40f; // 增加重力
    public float maxSpeedOnGround = 5f;
    public float moveSharpnessOnGround = 15f;
    public float rotationSpeed = 200f;
    public float maxHealth = 200f;

    public float jumpForce = 5f; // 调整跳跃力
    public float cameraHeightRatio = 0.9f;

    private CharacterController _characterController;
    private PlayerInputHandler _inputHandler;
    private float _targetCharacterHeight = 1.8f;
    private float _cameraVerticalAngle = 0f;
    public float _currentHealth;
    private bool _isBossAttack;
    private bool _isJumping;
    private AudioSource _audioSource;
    private Animator _animator;
    private CharactorState newstate;
    private CharactorState oldstate;

    private enum CharactorState
    {
        walkTrigger,
        runTrigger,
        idleTrigger,
        jumpTrigger
    }

    public float CurrentHealth => _currentHealth;

    public Vector3 CharacterVelocity { get; set; }

    private void Awake()
    {
        Instance = this;
        _currentHealth = maxHealth;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _audioSource = GetComponent<AudioSource>();

        _characterController.enableOverlapRecovery = true;

        newstate = CharactorState.idleTrigger;
        oldstate = CharactorState.idleTrigger;

        UpdateCharacterHeight();
    }

    private void Update()
    {
        HandleCharacterMovement();
        HandleStateTransitions();
    }

    private void HandleStateTransitions()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newstate = CharactorState.runTrigger;
                maxSpeedOnGround = 10f;
            }
            else
            {
                newstate = CharactorState.walkTrigger;
                maxSpeedOnGround = 5f;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            newstate = CharactorState.idleTrigger;
        }

        if (_inputHandler.GetJumpInputDown() && _characterController.isGrounded)
        {
            newstate = CharactorState.jumpTrigger;
            _isJumping = true;
            StartCoroutine(OnWait()); // 启动协程
        }

        if (newstate != oldstate)
        {
            _animator.SetTrigger(newstate.ToString());
            oldstate = newstate;
        }
    }

    private IEnumerator OnWait()
    {
        yield return new WaitForSeconds(0.7f);
        _isJumping = false;
        _animator.SetTrigger("idleTrigger");
        newstate = CharactorState.idleTrigger;
        oldstate = newstate;
    }

    private void UpdateCharacterHeight()
    {
        _characterController.height = _targetCharacterHeight;
        _characterController.center = Vector3.up * _characterController.height * 0.5f;

        playerCamera.transform.localPosition = Vector3.up * _characterController.height * 0.9f;
    }

    private void HandleCharacterMovement()
    {
        // Camera rotate horizontal
        transform.Rotate(new Vector3(0, _inputHandler.GetMouseLookHorizontal() * rotationSpeed, 0), Space.Self);

        // Camera rotate vertical
        _cameraVerticalAngle += _inputHandler.GetMouseLookVertical() * rotationSpeed;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(-_cameraVerticalAngle, 0, 0);

        // Move
        Vector3 worldSpaceMoveInput = transform.TransformVector(_inputHandler.GetMoveInput());

        if (_characterController.isGrounded)
        {
            Vector3 targetVelocity = worldSpaceMoveInput * maxSpeedOnGround;
            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity, moveSharpnessOnGround * Time.deltaTime);

            if (_isJumping)
            {
                CharacterVelocity = new Vector3(CharacterVelocity.x, jumpForce, CharacterVelocity.z);
                _isJumping = false;
            }
        }
        else
        {
            CharacterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
        }

        if (_isBossAttack)
        {
            CharacterVelocity += transform.forward * -5f;
        }

        _characterController.Move(CharacterVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnHitPlayer(other.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHitPlayer(other);
    }

    private void OnHitPlayer(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Bullet enemybullet = other.GetComponent<Bullet>();
            _currentHealth -= enemybullet.damage;

            StartCoroutine(OnDamage());

            if (other.GetComponent<Rigidbody>())
            {
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("MeleeArea"))
        {
            MeleeAttacker meleeAttacker = other.GetComponent<MeleeAttacker>();
            _currentHealth -= meleeAttacker.damage;

            _isBossAttack = other.name == "Boss Melee Area";

            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        _audioSource.Play();

        if (_currentHealth < 0)
        {
            OnDie();
        }

        yield return new WaitForSeconds(0.2f);
        _isBossAttack = false;
    }

    private void OnDie()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
