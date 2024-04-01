using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        //apply the boost value to appropriate stats in the child set class
         
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
}
