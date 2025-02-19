using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;
    private PlayerCharacterController playerController;

    void Start()
    {
        // 找到玩家控制脚本
        playerController = FindObjectOfType<PlayerCharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false); // 隐藏暂停菜单
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked; // 锁定光标
        Cursor.visible = false; // 隐藏光标
        playerController.enabled = true; // 启用玩家控制脚本
        isPaused = false;
        if(GameManager.Instance != null)
        {
            GameManager.Instance._audioSource.Play();
        }
    }

    public void PauseGame()
    {
        pauseMenuCanvas.SetActive(true); // 显示暂停菜单
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; // 释放光标
        Cursor.visible = true; // 显示光标
        playerController.enabled = false; // 禁用玩家控制脚本
        isPaused = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance._audioSource.Pause();
        }
    }

    public void QuitGame()
    {
        // 如果在编辑器中运行，需要退出播放模式
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

