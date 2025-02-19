using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinGoldTrigger : MonoBehaviour
{
    public GameObject Gold;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isGetGold = true;
            Gold.SetActive(true);
            Destroy(Gold, 3f);
            Destroy(gameObject);
        }
    }
}
