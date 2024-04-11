using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObject/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField] private float multipler;
    [SerializeField] new string name;
    [SerializeField] string description;
    [SerializeField] private int level;
    [SerializeField] private GameObject nextLevelPrefab;
    [SerializeField] private Sprite icon;
    public float Multipler
    {
        get => multipler;
        private set => multipler = value;
    }

    public int Level
    {
        get => level;
        private set => level = value;
    }

    public GameObject NextLevelPrefab
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
