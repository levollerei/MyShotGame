using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliverEnemyGoTrigger : MonoBehaviour
{
    public GameObject AirWall;
    public GameObject KeyFloor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.silverRoomEnemies.Count == 0)
            {
                GameManager.Instance._audioSource.Stop();
                Destroy(AirWall);
                Destroy(KeyFloor);
                Destroy(gameObject);
            }
        }
    }
}
