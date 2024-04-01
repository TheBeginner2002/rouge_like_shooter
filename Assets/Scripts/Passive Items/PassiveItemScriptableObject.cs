using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObject/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField] private float multipler;
    [SerializeField] private int level;
    [SerializeField] private GameObject nextLevelPrefab;
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
}
