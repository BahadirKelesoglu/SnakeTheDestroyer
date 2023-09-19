using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headEat : MonoBehaviour
{
    int simplefood = 1;


    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("yedim");
            GameManager.Instance.coin += 1;
            Destroy(other.gameObject);
            levelDesign.Instance.addScore(simplefood);
            GameManager.Instance.Save();
        }

        if (other.gameObject.CompareTag("wall"))
        {
            Debug.Log("walla değdim");
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z+180f);
        }

        if (other.gameObject.CompareTag("enemy1Bullet"))
        {
            levelDesign.Instance.takenDamage += 1;
            Destroy(other.gameObject);
        }
    }
}
