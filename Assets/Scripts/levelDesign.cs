using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelDesign : MonoBehaviour
{
    private static levelDesign instance;
    public static levelDesign Instance { get { return instance; } }


    public GameObject foodPrefab;
    public float spawnTime = 1.0f;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public float meteorSpeed = 1.0f;


    private void Awake()
    {
        instance = this;
        score = 0;
    }

    void Start()
    {
        StartCoroutine(spawnFood());
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            
            if(child.position.x > 20f || child.position.x < -20f || child.position.y > 10f || child.position.y < -20f)
                Destroy(child.gameObject);
        }
    }


    IEnumerator spawnFood()
    {
        while (true) { 
        float randomx = Random.Range(-20f, 20f);
        float randomy = Random.Range(-20f, 10f);
            float randomVelocityx = Random.Range(-1f, 1f);
            float randomVelocityy = Random.Range(-1f, 1f);

            Vector3 spawnPosition = new Vector3(randomx, randomy, 0f);

        GameObject meteor = Instantiate(foodPrefab, spawnPosition, Quaternion.identity,transform);
        Rigidbody2D meteorRB =meteor.GetComponent<Rigidbody2D>();
        meteorRB.velocity = new Vector2(randomVelocityx,randomVelocityy) * meteorSpeed;
        Transform fireParticle = meteor.transform.GetChild(0);
        float fireAngle = Mathf.Atan2(meteorRB.velocity.y,meteorRB.velocity.x) * Mathf.Rad2Deg;

        fireParticle.rotation = Quaternion.Euler(fireAngle, -90, 0);

        yield return new WaitForSeconds(spawnTime);
        }
    }

    public void addScore(int scorePoint)
    {
        score += scorePoint;
        
    }

    public int getScore()
    {
        return score;
    }
}
