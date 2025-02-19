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
        // 更新小地图相机的位置
        minicamera.transform.position = new Vector3(player.position.x, minicamera.transform.position.y, player.position.z);

        // 更新小地图的旋转
        minicamera.transform.eulerAngles = new Vector3(90, player.eulerAngles.y, 0);
    }
}
