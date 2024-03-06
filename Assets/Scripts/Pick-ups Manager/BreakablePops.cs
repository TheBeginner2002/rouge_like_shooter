using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePops : MonoBehaviour
{
     public float health;

     public  void TakeDamage(float dmg)
     {
          health -= dmg;

          if (health <= 0)
          {
               Kill();
          }
     }

     void Kill()
     {
          Destroy(gameObject);
     }
}
