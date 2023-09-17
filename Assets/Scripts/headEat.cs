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
        if(other.gameObject.CompareTag("Food"))
        {
            GameManager.Instance.coin += 1;
            Destroy(other.gameObject);
            levelDesign.Instance.addScore(simplefood);
            GameManager.Instance.Save();
        }
    }
}
