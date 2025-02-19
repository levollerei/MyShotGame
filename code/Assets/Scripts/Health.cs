using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isInstantDie;
    public float maxHealth = 100f;

    public bool isDead { get; private set; }

    private float _currentHealth;

    private MeshRenderer[] _meshes;
    private Animator _animator;

    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _meshes = GetComponentsInChildren<MeshRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

        StartCoroutine(OnDamage());
    }

    private IEnumerator OnDamage()
    {
        foreach (MeshRenderer mesh in _meshes)
        {
            mesh.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        if (_currentHealth > 0)
        {
            foreach (MeshRenderer mesh in _meshes)
            {
                mesh.material.color = Color.white;
            }
        }
        else if (!isDead)
        {
            foreach (MeshRenderer mesh in _meshes)
            {
                mesh.material.color = Color.gray;
            }

            isDead = true;

            if (isInstantDie)
            {
                OnDestroy();
                Destroy(gameObject);
            }
            else
            {
                OnDestroy();
                _animator.SetTrigger("doDie");
                Destroy(gameObject, 3);
            }
        }
    }

    private void OnDestroy()
    {
        // 从 GameManager.Instance.basicRoomEnemies 中剔除该对象
        Enemy enemyComponent = GetComponent<Enemy>();
        if (enemyComponent != null && GameManager.Instance != null)
        {
            if(GameManager.Instance.basicRoomEnemies.Contains(enemyComponent))
            {
                GameManager.Instance.basicRoomEnemies.Remove(enemyComponent);
            }
            if (GameManager.Instance.silverRoomEnemies.Contains(enemyComponent))
            {
                GameManager.Instance.silverRoomEnemies.Remove(enemyComponent);
            }
            if (GameManager.Instance.bronzeRoomEnemies.Contains(enemyComponent))
            {
                GameManager.Instance.bronzeRoomEnemies.Remove(enemyComponent);
            }
            if (GameManager.Instance.goldRoomEnemies.Contains(enemyComponent))
            {
                GameManager.Instance.goldRoomEnemies.Remove(enemyComponent);
            }
        }
    }
}
