using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverRoomTrigger : MonoBehaviour
{
    public AudioClip BasicFightMusic;
    public GameObject AirWall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(AirWall);
            GameManager.Instance._audioSource.clip = BasicFightMusic;
            GameManager.Instance.TriggerSilverEnemies();
            Destroy(gameObject);
        }
    }
}
