using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrithRoomTrigger : MonoBehaviour
{
    public GameObject AirWall;
    public GameObject talk;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(KeyNPCManager.instance.isFirstTalk)
            {
                StartCoroutine(Show());
            }
            else
            {
                Destroy(AirWall);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Show()
    {
        talk.SetActive(true);
        yield return new WaitForSeconds(3f);
        talk.SetActive(false);
    }
}
