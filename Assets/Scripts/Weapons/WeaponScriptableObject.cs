using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObject/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] new string name;
    [SerializeField] string description;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float cooldownDuration;
    [SerializeField] int pierce;
    [SerializeField] int level;
    [SerializeField] GameObject nextLevelPrefab;
    [SerializeField] Sprite icon;

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
        set => pierce = value;
    }

    public int Level //not modified in game,only on editor
    {
        get => level;
        set => level = value;
    }

    public GameObject NextLevelPrefab //the prefab weapon for the next level
    {
        get => nextLevelPrefab;
        set => nextLevelPrefab = value;
    }

    public Sprite Icon
    {
        get => icon;
        set => icon = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Description
    {
        get => description;
        set => description = value;
    }
}
