using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tail : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy1Bullet"))
        {
            levelDesign.Instance.takenDamage += 1;
            Destroy(other.gameObject);
        }

        
    }
    }
