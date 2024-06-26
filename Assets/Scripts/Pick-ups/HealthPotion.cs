using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickups
{
    public int healthToRestore;

    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.TakeHealth(healthToRestore);
    }
}
