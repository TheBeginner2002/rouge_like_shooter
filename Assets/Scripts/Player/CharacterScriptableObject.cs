using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObject/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private new string name;
    [SerializeField] private GameObject startingWeapon;
    [SerializeField] private float maxHealth;
    [SerializeField] private float recovery;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float might;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float magnet;

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

    public GameObject StartingWeapon
    {
        get => startingWeapon;
        set => startingWeapon = value;
    }

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float Recovery
    {
        get => recovery;
        set => recovery = value;
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public float Might
    {
        get => might;
        set => might = value;
    }

    public float ProjectileSpeed
    {
        get => projectileSpeed;
        set => projectileSpeed = value;
    }

    public float Magnet
    {
        get => magnet;
        set => magnet = value;
    }
}
