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
        // �ҵ���ҿ��ƽű�
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
        pauseMenuCanvas.SetActive(false); // ������ͣ�˵�
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked; // �������
        Cursor.visible = false; // ���ع��
        playerController.enabled = true; // ������ҿ��ƽű�
        isPaused = false;
        if(GameManager.Instance != null)
        {
            GameManager.Instance._audioSource.Play();
        }
    }

    public void PauseGame()
    {
        pauseMenuCanvas.SetActive(true); // ��ʾ��ͣ�˵�
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; // �ͷŹ��
        Cursor.visible = true; // ��ʾ���
        playerController.enabled = false; // ������ҿ��ƽű�
        isPaused = true;
        if (GameManager.Instance != null)
        {
            GameManager.Instance._audioSource.Pause();
        }
    }

    public void QuitGame()
    {
        // ����ڱ༭�������У���Ҫ�˳�����ģʽ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

