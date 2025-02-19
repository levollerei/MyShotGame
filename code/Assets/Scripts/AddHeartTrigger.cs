using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHeartTrigger : MonoBehaviour
{
    public GameObject HP;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HP.SetActive(true);
            PlayerCharacterController.Instance._currentHealth += 50;
            if(PlayerCharacterController.Instance._currentHealth > 200)
            {
                PlayerCharacterController.Instance._currentHealth = 200;
            }
            Destroy(HP, 3f);
            Destroy(gameObject);
        }
    }
}
