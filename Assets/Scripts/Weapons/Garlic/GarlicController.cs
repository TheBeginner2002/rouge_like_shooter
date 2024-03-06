using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnGarlic = Instantiate(weaponData.WeaponPrefab);
        spawnGarlic.transform.position = transform.position;
        spawnGarlic.transform.parent = transform;
    }
}
