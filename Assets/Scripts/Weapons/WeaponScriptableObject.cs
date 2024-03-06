using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObject/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float cooldownDuration;
    [SerializeField] int pierce;

    public GameObject WeaponPrefab
    {
        get => weaponPrefab;
        set => weaponPrefab = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float CooldownDuration
    {
        get => cooldownDuration;
        set => cooldownDuration = value;
    }

    public int Pierce
    {
        get => pierce;
        set =>  pierce = value;
    }
}
