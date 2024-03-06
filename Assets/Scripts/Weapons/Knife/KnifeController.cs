using UnityEngine;

public class KnifeController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();

        GameObject spawnKnife = Instantiate(weaponData.WeaponPrefab);
        spawnKnife.transform.position = transform.position;
        spawnKnife.GetComponent<KnifeBehavior>().DirectionChecker(player.lastMoveDir);
    }
}
