using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyGoTrigger : MonoBehaviour
{
    public GameObject AirWall;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(GameManager.Instance.basicRoomEnemies.Count == 0)
            {
                GameManager.Instance._audioSource.Stop();
                Destroy(AirWall);
                Destroy(gameObject);
            }
        }
    }
}
