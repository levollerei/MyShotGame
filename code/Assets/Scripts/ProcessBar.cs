using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProcessBar : MonoBehaviour
{
    public Image processBarImage;
    public float MaxProcessTime = 5f;
    public string nextSceneName;

    private float NowProcessTime;
    private float startTime;

    private void Start()
    {
        NowProcessTime = 0f;
        startTime = Time.time;
    }

    private void Update()
    {
        NowProcessTime = Time.time - startTime;
        if (NowProcessTime < MaxProcessTime)
        {
            processBarImage.fillAmount = NowProcessTime / MaxProcessTime;
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
