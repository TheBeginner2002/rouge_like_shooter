using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    [SerializeField] private float destroyAfterSeconds;

    private float _currentDamage;
    private float _currentSpeed;
    private float _currentCooldownDuration;
    private float _currentPierce;

    public float CurrentDamage
    {
        get => _currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
        set => _currentDamage = value;
    }

    public float CurrentSpeed
    {
        get => _currentSpeed;
        set => _currentSpeed = value;
    }

    public float CurrentCooldownDuration
    {
        get => _currentCooldownDuration;
        set => _currentCooldownDuration = value;
    }

    public float CurrentPierce
    {
        get => _currentPierce;
        set => _currentPierce = value;
    }

    private void Awake()
    {
        _currentDamage = weaponData.Damage;
        _currentSpeed = weaponData.Speed;
        _currentCooldownDuration = weaponData.CooldownDuration;
        _currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject,destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamge(_currentDamage);
        }
        else if (other.CompareTag("Prop") && other.gameObject.TryGetComponent(out BreakablePops breakablePops))
        {
            breakablePops.TakeDamage(_currentDamage);
        }
    }
}
