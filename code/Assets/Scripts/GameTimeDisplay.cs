using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // 确保引用了UnityEngine.UI命名空间

public class TimeManager : MonoBehaviour
{
    public Text timeText;  // 确保这是Text类型
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");

        timeText.text = "Time: " + minutes + ":" + seconds;
    }
}
