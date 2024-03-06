using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.position += direction * (CurrentSpeed * Time.deltaTime); 
    }
}
