using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyNPCManager : MonoBehaviour
{
    public static KeyNPCManager instance;

    public GameObject talk1;
    public GameObject talk2;
    public GameObject talk3;
    public GameObject talk4;

    private Animator _animator;

    public bool isFirstTalk = true;

    private void Awake()
    {
        instance = this;
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger("doHello");
            if(!GameManager.Instance.isGetKey)
            {
                if(GameManager.Instance.isGetBronze && GameManager.Instance.isGetSilver && GameManager.Instance.isGetGold)
                {
                    GameManager.Instance.isGetKey = true;
                    StartCoroutine(Show2());
                }
                else
                {
                    StartCoroutine(Show1());
                }
            }
        }
    }

    IEnumerator Show1()
    {
        yield return new WaitForSeconds(2.5f);
        if (isFirstTalk)
        {
            _animator.SetTrigger("doTalk");
            talk1.SetActive(true);
            yield return new WaitForSeconds(2f);
            talk1.SetActive(false);
            _animator.SetTrigger("doTalk");
            talk2.SetActive(true);
            yield return new WaitForSeconds(2f);
            talk2 .SetActive(false);
            _animator.SetTrigger("doTalk");
            talk3.SetActive(true);
            yield return new WaitForSeconds(2f);
            talk3.SetActive(false);
            isFirstTalk = false;
        }
        else
        {
            _animator.SetTrigger("doTalk");
            talk2.SetActive(true);
            yield return new WaitForSeconds(2f);
            talk2.SetActive(false);
            _animator.SetTrigger("doTalk");
            talk3.SetActive(true);
            yield return new WaitForSeconds(2f);
            talk3.SetActive(false);
        }
    }

    IEnumerator Show2()
    {
        yield return new WaitForSeconds(2.5f);
        _animator.SetTrigger("doTalk");
        talk4.SetActive(true);
        yield return new WaitForSeconds(3f);
        talk4.SetActive(false);
    }
}
