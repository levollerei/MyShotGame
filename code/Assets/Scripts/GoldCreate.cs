using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCreate : MonoBehaviour
{
    public GameObject GoldCoin;
    private bool isCreateGold = false;
    private Transform m_transform;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.goldRoomEnemies.Count == 0)
        {
            if (isCreateGold == false)
            {
                GoldCoin.SetActive(true);
                isCreateGold = true;
            }
        }
    }
}
