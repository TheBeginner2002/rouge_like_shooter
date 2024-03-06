using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stat")]
    public WeaponScriptableObject weaponData;
    
    private float _currentCooldown;
    protected PlayerMovement player;
    
    protected virtual void Start()
    {
        _currentCooldown = weaponData.CooldownDuration;
        player = FindObjectOfType<PlayerMovement>();
    }

    protected virtual void Update()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown <= 0f) 
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        _currentCooldown = weaponData.CooldownDuration;
    }
}
