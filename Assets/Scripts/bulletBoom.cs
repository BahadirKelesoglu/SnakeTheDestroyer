using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class bulletBoom : MonoBehaviour
{
    public int damage = 2;
    [SerializeField] AudioSource boomSound;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy1"))
        {

            boomSound.Play();
            
            int enemyIndex = Enemy.Instance.Enemies.IndexOf(other.gameObject);

            
            if (enemyIndex >= 0 && enemyIndex < Enemy.Instance.enemyHealthList.Count)
            {
                
                Enemy.Instance.enemyHealthList[enemyIndex] -= damage;

                
                if (Enemy.Instance.enemyHealthList[enemyIndex] <= 0)
                {
                    Destroy(other.gameObject);
                    Enemy.Instance.enemyHealthList.RemoveAt(enemyIndex);
                    Enemy.Instance.Enemies.RemoveAt(enemyIndex);
                }

                other.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = (float)Enemy.Instance.enemyHealthList[enemyIndex] / (float)Enemy.Instance.enemyHealth;
            }
            

            
            Destroy(gameObject);
        }


    }
}
