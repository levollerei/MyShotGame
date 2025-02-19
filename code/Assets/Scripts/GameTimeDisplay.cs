using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // ȷ��������UnityEngine.UI�����ռ�

public class TimeManager : MonoBehaviour
{
    public Text timeText;  // ȷ������Text����
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
