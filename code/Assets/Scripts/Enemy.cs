using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { Melee, Range, Boss}

    public Type enemyType;

    public Transform target;
    public BoxCollider meleeArea;
    public GameObject bullet;

    protected Rigidbody _rigidbody;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected Health _health;

    private float _targetRadius;
    private float _targetRange;
    private bool _isChase;
    private bool _isAttack;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _health = GetComponent<Health>();

        //if(enemyType != Type.Boss)
        //{
        //    Invoke("ChaseStart", 2);
        //}
    }

    public void ChaseStart()
    {
        _isChase = true;

        if(_animator)
        {
            _animator.SetBool("isWalk", true);
        }
    }

    private void FreeVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        if(_health.isDead)
        {
            StopAllCoroutines();
        }

        if (_navMeshAgent.enabled && !_health.isDead && enemyType != Type.Boss)
        {
            _navMeshAgent.SetDestination(target.position);
            _navMeshAgent.isStopped = !_isChase;
        }

        if (_health.isDead)
        {
            // 敌人死亡后移除其碰撞体
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if(_isChase)
        {
            FreeVelocity();
        }

        if(enemyType != Type.Boss)
        {
            Targeting();
        }
    }

    private void Targeting()
    {
        if(enemyType == Type.Melee)
        {
            _targetRadius = 0.5f;
            _targetRange = 2f;
        }
        else if (enemyType == Type.Range)
        {
            _targetRadius = 0.5f;
            _targetRange = 10f;
        }

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 
            _targetRadius, transform.forward, _targetRange, LayerMask.GetMask("Player"));

        if (hits.Length > 0 && !_isAttack && !_health.isDead)
        {
            // 携程
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        _isChase = false;
        _isAttack = true;

        _animator.SetBool("isAttack", true);

        if(enemyType == Type.Melee)
        {
            yield return new WaitForSeconds(0.2f);

            meleeArea.enabled = true;

            yield return new WaitForSeconds(1f);

            meleeArea.enabled = false;

            yield return new WaitForSeconds(1f);
        }
        else if(enemyType == Type.Range)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rigidbodyBullet = instantBullet.GetComponent<Rigidbody>();
            rigidbodyBullet.velocity = transform.forward * 20f;

            yield return new WaitForSeconds(2f);
        }

        _isChase = true;
        _isAttack = false;
        _animator.SetBool("isAttack",false);
    }
}
