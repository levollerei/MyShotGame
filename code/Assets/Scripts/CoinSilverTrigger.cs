using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSilverTrigger : MonoBehaviour
{
    public GameObject outdoor;
    public GameObject Silver;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isGetSilver = true;
            Silver.SetActive(true);
            Destroy(Silver, 3f);
            Destroy(outdoor);
            Destroy(gameObject);
        }
    }
}
