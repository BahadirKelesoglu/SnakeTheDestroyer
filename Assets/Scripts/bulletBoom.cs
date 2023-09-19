using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy1"))
        {
            
            Destroy(other.gameObject);

            Enemy.Instance.Enemies.Remove(other.gameObject);

            Destroy(gameObject);
        }


    }
}
