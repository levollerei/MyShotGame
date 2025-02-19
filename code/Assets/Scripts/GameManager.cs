using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gamePanel;
    public RectTransform anchoredBossHealthBar;
    public AudioSource _audioSource;

    public List<Enemy> basicRoomEnemies;
    public List<Enemy> bossRoomEnemies;
    public List<Enemy> silverRoomEnemies;
    public List<Enemy> bronzeRoomEnemies;
    public List<Enemy> goldRoomEnemies;

    public bool isGetBronze = false;
    public bool isGetSilver = false;
    public bool isGetGold = false;
    public bool isGetKey = false;

    private void Awake()
    {
        Instance = this;
        gamePanel.SetActive(true);
        anchoredBossHealthBar.anchoredPosition = Vector3.up * 100f;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position = PlayerCharacterController.Instance.transform.position;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("LoadSceneToEnd");
    }

    public void TriggerBasicEnemies()
    {
        _audioSource.Stop();
        _audioSource.Play();
        foreach (Enemy basicRoomEnemy in basicRoomEnemies)
        {
            basicRoomEnemy.ChaseStart();
        }
    }

    public void TriggerSilverEnemies()
    {
        _audioSource.Stop();
        _audioSource.Play();
        foreach (Enemy silverRoomEnemy in silverRoomEnemies)
        {
            silverRoomEnemy.ChaseStart();
        }
    }

    public void TriggerBronzeEnemies()
    {
        _audioSource.Stop();
        _audioSource.Play();
        foreach (Enemy bronzeRoomEnemy in bronzeRoomEnemies)
        {
            bronzeRoomEnemy.ChaseStart();
        }
    }

    public void TriggerGoldEnemies()
    {
        _audioSource.Stop();
        _audioSource.Play();
        foreach (Enemy goldRoomEnemy in goldRoomEnemies)
        {
            goldRoomEnemy.ChaseStart();
        }
    }

    public void TriggerBoss()
    {
        _audioSource.Stop();
        _audioSource.Play();
        anchoredBossHealthBar.anchoredPosition = Vector3.down * 90f;
        
        foreach (var enemy in bossRoomEnemies)
        {
            Boss boss = enemy as Boss;
            if(boss)
            {
                boss.StartAttack();
            }
        }
    }

    public void TriggerBegin()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }

    public void TriggerWin()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }
}
