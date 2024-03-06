using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyData;

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

    void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        Debug.Log("On Player Col");
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(_currentDamage);
        }
    }
}


