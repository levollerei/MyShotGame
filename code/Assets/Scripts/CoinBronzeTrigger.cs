using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBronzeTrigger : MonoBehaviour
{
    public GameObject Bronze;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isGetBronze = true;
            Bronze.SetActive(true);
            Destroy(Bronze, 3f);
            Destroy(gameObject);
        }
    }
}
