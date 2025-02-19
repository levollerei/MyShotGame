using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginTrigger : MonoBehaviour
{
    public AudioClip BeginMusic;
    private bool isBegin;

    private void Start()
    {
        isBegin = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isBegin == false)
        {
            isBegin = true;
            GameManager.Instance._audioSource.clip = BeginMusic;
            GameManager.Instance.TriggerBegin();
            Destroy(gameObject);
        }
    }
}
