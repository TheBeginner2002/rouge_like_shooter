using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector Instance;
    public CharacterScriptableObject characterData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Extra "+ this + "Delete");
            Destroy(gameObject);
        }
    }

    public static CharacterScriptableObject GetData() => Instance.characterData;

    public void SelectCharacter(CharacterScriptableObject character) => characterData = character;

    public void DestroySingleton()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
