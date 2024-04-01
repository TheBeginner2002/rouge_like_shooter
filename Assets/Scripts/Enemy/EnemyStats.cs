using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyData;
    public float despawnDistance = 20f;
    private Transform player;
    

    private float _currentSpeed;
    private float _currentHealth;
    private float _currentDamage;

    public float CurrentSpeed
    {
        get => _currentSpeed;
        set => _currentSpeed = value;
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public float CurrentDamage
    {
        get => _currentDamage;
        set => _currentDamage = value;
    }

    private void Awake()
    {
        _currentSpeed = enemyData.MoveSpeed;
        _currentHealth = enemyData.MaxHealth;
        _currentDamage = enemyData.Damage;
    }

    public void TakeDamge(float dmg)
    {
        _currentHealth -= dmg;

        if (_currentHealth <= 0f)
        {
             Kill();
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        // Debug.Log("On Player Col");
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(_currentDamage);
        }
    }

    private void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyDied();
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position =
            player.position + es.relativeSpawnPoint[Random.Range(0, es.relativeSpawnPoint.Count)].position;
    }
}



