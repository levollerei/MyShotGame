using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    public bool isLook;

    public GameObject missileBoss;
    public GameObject Portal;
    public Transform missilePortA;
    public Transform missilePortB;
    public Transform portalPort;
    public AudioClip winMusic;

    private Vector3 _loockVec;
    private BoxCollider _boxcollider;
    private Vector3 _jumpHitTarget;
    private bool isGeneratePortal;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _health = GetComponent<Health>();
        _boxcollider = GetComponent<BoxCollider>();

        _navMeshAgent.isStopped = true;
        isGeneratePortal = false;

        //StartCoroutine(Think());
    }

    public void StartAttack()
    {
        StartCoroutine(Think());
    }

    private void Update()
    {
        if(_health.isDead)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StopAllCoroutines();
            if(isGeneratePortal == false)
            {
                GameManager.Instance._audioSource.clip = winMusic;
                GameObject portal = Instantiate(Portal, portalPort.position, portalPort.rotation);
                portal.name = Portal.name;
                isGeneratePortal = true;
            }
            //GameManager.Instance.EndGame();
        }
        else
        {
            if (isLook)
            {
                float horizon = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                _loockVec = new Vector3(horizon, 0, vertical) * 3f;
                transform.LookAt(target.position + _loockVec);
            }
            else
            {
                _navMeshAgent.SetDestination(_jumpHitTarget);
            }
        }
    }

    private IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = UnityEngine.Random.Range(0, 5);

        switch(ranAction)
        {
            case 0: case 1: case 2: case 3:
                StartCoroutine(MissileShot()); 
                break;
            case 4:
                StartCoroutine(JumpHit());
                break;
        }
    }

    private IEnumerator MissileShot()
    {
        _animator.SetTrigger("doShot");

        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missileBoss, missilePortA.position, missilePortA.rotation);
        MissileBoss missileBossA = instantMissileA.GetComponent<MissileBoss>();
        missileBossA.target = target;

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missileBoss, missilePortB.position, missilePortB.rotation);
        MissileBoss missileBossB = instantMissileB.GetComponent<MissileBoss>();
        missileBossB.target = target;

        yield return new WaitForSeconds(3f);
        if (instantMissileA != null)
        {
            Destroy(instantMissileA);
        }
        if (instantMissileB != null)
        {
            Destroy(instantMissileB);
        }

        StartCoroutine(Think());
    }

    private IEnumerator JumpHit()
    {
        _jumpHitTarget = target.position + _loockVec;

        isLook = false;
        _navMeshAgent.isStopped = false;
        _boxcollider.enabled = false;

        _animator.SetTrigger("doJumpHit");

        yield return new WaitForSeconds(1.5f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);

        isLook = true;
        _navMeshAgent.isStopped = true;
        _boxcollider.enabled = true;

        yield return new WaitForSeconds(3f);
        StartCoroutine(Think());
    }
}
