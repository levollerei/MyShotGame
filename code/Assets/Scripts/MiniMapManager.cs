using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public Camera minicamera;
    public Transform player;
    public Transform miniplayerIcon;
    public GameObject enemyIconPrefab;

    void Start()
    {

    }

    void Update()
    {
        // ����С��ͼ�����λ��
        minicamera.transform.position = new Vector3(player.position.x, minicamera.transform.position.y, player.position.z);

        // ����С��ͼ����ת
        minicamera.transform.eulerAngles = new Vector3(90, player.eulerAngles.y, 0);
    }
}
