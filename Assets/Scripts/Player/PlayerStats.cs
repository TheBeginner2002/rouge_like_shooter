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

    private InventoryManager _inventoryManager;
    public int weaponIndex;
    public int passiveItemIndex;

    public GameObject firstPassiveItemTest, secondPassiveItemTest;
    
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    #region Get Set
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (GameManager.instance)
            {
                GameManager.instance.currentHealthDisplay.text = "Health: " + _currentHealth;
            }
        }
    }

    public float CurrentRecovery
    {
        get => _currentRecovery;
        set
        {
            _currentRecovery = value;
            if (GameManager.instance)
            {
                GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + _currentRecovery;
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get => _currentMoveSpeed;
        set
        {
            _currentMoveSpeed = value;
            if (GameManager.instance)
            {
                GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + _currentMoveSpeed;
            }
        }
    }

    public float CurrentMight
    {
        get => _currentMight;
        set
        {
            _currentMight = value;
            if (GameManager.instance)
            {
                GameManager.instance.currentMightDisplay.text = "Might: " + _currentMight;
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get => _currentProjectileSpeed;
        set
        {
            _currentProjectileSpeed = value;
            if (GameManager.instance)
            {
                GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + _currentProjectileSpeed;
            }
        }
    }

    public float CurrentMagnet
    {
        get => _currentMagnet;
        set {
            _currentMagnet = value;
            if (GameManager.instance)
            {
                GameManager.instance.currentMagnetDisplay.text = "Magnet: " + _currentMagnet;
            }
        }
    }
    #endregion

    private void Awake()
    {
        _characterData = CharacterSelector.GetData();
        CharacterSelector.Instance.DestroySingleton();
        _inventoryManager = GetComponent<InventoryManager>();
        
        CurrentHealth = _characterData.MaxHealth;
        CurrentRecovery = _characterData.Recovery;
        CurrentMoveSpeed = _characterData.MoveSpeed;
        CurrentMight = _characterData.Might;
        CurrentProjectileSpeed = _characterData.ProjectileSpeed;
        CurrentMagnet = _characterData.Magnet;
        
        SpawnWeapon(_characterData.StartingWeapon);
        
        SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);
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
            CurrentHealth -= dmg;
        
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
        CurrentHealth += health;
        
        if (CurrentHealth >= _characterData.MaxHealth)
        {
            CurrentHealth = _characterData.MaxHealth;
        }
    }

    void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemUI(_inventoryManager.weaponUISlots,_inventoryManager.passiveItemUISLots);
            GameManager.instance.GameOver();
        }
    }

    void Recover()
    {
        if (CurrentHealth < _characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if (CurrentHealth >= _characterData.MaxHealth)
            {
                CurrentHealth = _characterData.MaxHealth;
            }
        }
    }

    private void Start()
    {
        experienceCap = LevelRanges[0].experienceCapIncrease;
        
        //set stats display
        GameManager.instance.currentHealthDisplay.text = "Health: " + _currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + _currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + _currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + _currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + _currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + _currentMagnet;
        
        GameManager.instance.AssignChosenCharacterUI(_characterData);
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
        //checking is slots of weapon is full
        if (weaponIndex >= _inventoryManager.weaponSlots.Count - 1)
        {
            Debug.Log("Inventory slots is full");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); 
        _inventoryManager.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
        weaponIndex++;
    }
    
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        //checking is slots of weapon is full
        if (passiveItemIndex >= _inventoryManager.passiveItemSlots.Count - 1)
        {
            Debug.Log("Passive item is full");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform); 
        _inventoryManager.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex++;
    }
}
