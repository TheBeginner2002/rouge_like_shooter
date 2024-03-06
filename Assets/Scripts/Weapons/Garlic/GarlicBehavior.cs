using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehavior : MeleeWeaponBehavior
{
    private List<GameObject> _markedEnemies;
    
    protected override void Start()
    {
        base.Start();
        _markedEnemies = new List<GameObject>();
    }


    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !_markedEnemies.Contains(other.gameObject))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamge(CurrentDamage);
            
            _markedEnemies.Add(other.gameObject);
        }
        else if (other.CompareTag("Prop") 
                 && other.gameObject.TryGetComponent(out BreakablePops breakablePops) 
                 && !_markedEnemies.Contains(other.gameObject))
        {
            breakablePops.TakeDamage(CurrentDamage);
            
            _markedEnemies.Add(other.gameObject);
        }
    }
}
