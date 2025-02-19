using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronzeEnemyTrigger : MonoBehaviour
{
    public AudioClip BasicFightMusic;
    public GameObject AirWall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(AirWall);
            GameManager.Instance._audioSource.clip = BasicFightMusic;
            GameManager.Instance.TriggerBronzeEnemies();
            Destroy(gameObject);
        }
    }
}
