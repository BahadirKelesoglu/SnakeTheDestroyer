using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class headEat : MonoBehaviour
{
    public int simplefood = 1;
    public GameObject MenuPanel;
    public GameObject ContinueButton;
    public TextMeshProUGUI winOrLoseText;


    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            
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

        if (other.gameObject.CompareTag("Enemy1"))
        {
            levelDesign.Instance.takenDamage += 5;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("EnemyBoss"))
        {
            Destroy(transform.gameObject);
            
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            MenuPanel.SetActive(true);
            ContinueButton.SetActive(false);
            Time.timeScale = 0f;
            winOrLoseText.text = "WIN";

        }
    }
}
