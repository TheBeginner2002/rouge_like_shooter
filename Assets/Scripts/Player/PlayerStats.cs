using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private CharacterScriptableObject _characterData;

    private float _currentHealth;
    private float _currentRecovery;
    private float _currentMoveSpeed;
    private float _currentMight;
    private float _currentProjectileSpeed;
    private float _currentMagnet;
    
    [Header("Experience/Level")]
    [SerializeField] int experience = 0;
    [SerializeField] int level = 1;
    [SerializeField] int experienceCap;
    
    public List<LevelRange> LevelRanges;

    [Header("Experience/Level")] 
    public float invincibilityDuration;
    private float _invincibilityTimer;
    private bool _isInvincible;

    [Header("Spawn Weapon")] 
    public List<GameObject> spawnedWeapons;
    
    
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public float CurrentRecovery
    {
        get => _currentRecovery;
        set => _currentRecovery = value;
    }

    public float CurrentMoveSpeed
    {
        get => _currentMoveSpeed;
        set => _currentMoveSpeed = value;
    }

    public float CurrentMight
    {
        get => _currentMight;
        set => _currentMight = value;
    }

    public float CurrentProjectileSpeed
    {
        get => _currentProjectileSpeed;
        set => _currentProjectileSpeed = value;
    }

    public float CurrentMagnet
    {
        get => _currentMagnet;
        set => _currentMagnet = value;
    }

    private void Awake()
    {
        _characterData = CharacterSelector.GetData();
        CharacterSelector.Instance.DestroySingleton();
        
        _currentHealth = _characterData.MaxHealth;
        _currentRecovery = _characterData.Recovery;
        _currentMoveSpeed = _characterData.MoveSpeed;
        _currentMight = _characterData.Might;
        _currentProjectileSpeed = _characterData.ProjectileSpeed;
        _currentMagnet = _characterData.Magnet;
        
        SpawnWeapon(_characterData.StartingWeapon);
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (var range in LevelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease; 
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!_isInvincible)
        {
            _currentHealth -= dmg;
        
            _invincibilityTimer = invincibilityDuration;
            _isInvincible = true;
            
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void TakeHealth(int health)
    {
        _currentHealth += health;
        
        if (_currentHealth >= _characterData.MaxHealth)
        {
            _currentHealth = _characterData.MaxHealth;
        }
    }

    void Kill()
    {
        Debug.Log("Player is dead");
    }

    void Recover()
    {
        if (_currentHealth < _characterData.MaxHealth)
        {
            _currentHealth += _currentRecovery * Time.deltaTime;
            if (_currentHealth >= _characterData.MaxHealth)
            {
                _currentHealth = _characterData.MaxHealth;
            }
        }
    }

    private void Start()
    {
        experienceCap = LevelRanges[0].experienceCapIncrease;
    }

    private void Update()
    {
        if (_invincibilityTimer > 0)
        {
            _invincibilityTimer -= Time.deltaTime;
        }
        else if (_isInvincible)
        {
            _isInvincible = false;
        }
        
        Recover();
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); 
        spawnedWeapons.Add(spawnedWeapon);
    }
}
