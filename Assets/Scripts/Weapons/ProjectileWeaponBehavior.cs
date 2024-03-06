using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    [SerializeField] protected float destroyAfterSeconds;

    private float _currentDamage;
    private float _currentSpeed;
    private float _currentCooldownDuration;
    private float _currentPierce;

    public float CurrentDamage
    {
        get => _currentDamage;
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


    protected virtual void Start()
    {
        Destroy(gameObject,destroyAfterSeconds);
    }

    private void Awake()
    {
        _currentDamage = weaponData.Damage;
        _currentSpeed = weaponData.Speed;
        _currentCooldownDuration = weaponData.CooldownDuration;
        _currentPierce = weaponData.Pierce;
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirX = direction.x;
        float dirY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirX < 0 && dirY == 0) //left
        {
            ChangeDirection(ref scale,-1,-1);
        }
        else if (dirX == 0 && dirY < 0) //down
        {
            ChangeDirection(ref scale,1,-1);
        }
        else if (dirX == 0 && dirY > 0) //up
        {
            ChangeDirection(ref scale,-1,1);
        }
        else if (dirX > 0 && dirY > 0) //right up
        {
            rotation.z = 0f;
        }
        else if (dirX > 0 && dirY < 0) //right down
        {
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY > 0) //left up
        {
            ChangeDirection(ref scale,-1,-1);
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY < 0)
        {
            ChangeDirection(ref scale,-1,-1);
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    void ChangeDirection(ref Vector3 scale,float x,float y)
    {
        scale.x = scale.x * x;
        scale.y = scale.y * y;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamge(_currentDamage);
            ReducePierce();
        }
        else if (other.CompareTag("Prop") && other.gameObject.TryGetComponent(out BreakablePops breakablePops))
        {
            breakablePops.TakeDamage(_currentDamage);
            ReducePierce();
        }
    }

    void ReducePierce()
    {
        _currentPierce--;
        if (_currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
