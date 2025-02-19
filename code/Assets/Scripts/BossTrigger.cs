using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public AudioClip BossFightMusic;
    public GameObject AirWall;
    public GameObject Sorry;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(name == "TriggerBoss1")
            {
                Destroy(AirWall);
                GameManager.Instance._audioSource.clip = BossFightMusic;
                GameManager.Instance.TriggerBoss();
                Destroy(gameObject);
            }
            else
            {
                if (GameManager.Instance.isGetKey)
                {
                    Destroy(AirWall);
                    GameManager.Instance._audioSource.clip = BossFightMusic;
                    GameManager.Instance.TriggerBoss();
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(Show());
                }
            }
        }
    }

    IEnumerator Show()
    {
        Sorry.SetActive(true);
        yield return new WaitForSeconds(3f);
        Sorry.SetActive(false);
    }
}
