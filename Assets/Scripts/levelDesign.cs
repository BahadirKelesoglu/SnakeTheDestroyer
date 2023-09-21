using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelDesign : MonoBehaviour
{
    private static levelDesign instance;
    public static levelDesign Instance { get { return instance; } }

    public GameObject[] laserBorders;
    public GameObject endGameBorder;
    public GameObject beginningBorder;
    public GameObject blackHole;
    public GameObject foodPrefab;
    public GameObject runArrow;
    private bool arrowSetActiveFlag = false;
    public float spawnTime = 1.0f;
    [SerializeField] int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public float meteorSpeed = 1.0f;
    public int takenDamage = 0;
    public int BossTime = 5;
    



    private void Awake()
    {
        
        instance = this;
        score = 0;
    }

    void Start()
    {
        endGameBorder.SetActive(false);
        beginningBorder.SetActive(true);
        blackHole.SetActive(false);
        runArrow.SetActive(false);

        foreach (GameObject laser in laserBorders)
        {
            laser.SetActive(true);
        }

        StartCoroutine(spawnFood());
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        coinText.text = "Coin:" + GameManager.Instance.coin;

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            
            if(child.position.x > 20f || child.position.x < -20f || child.position.y > 10f || child.position.y < -20f)
                Destroy(child.gameObject);
        }
       

        if(score >= BossTime)
        {
            blackHole.SetActive(true);
            if(!arrowSetActiveFlag)
            runArrow.SetActive(true);
            arrowSetActiveFlag = true;
            endGameBorder.SetActive(true);
            beginningBorder?.SetActive(false);
            foreach(GameObject laser in laserBorders)
            {
                laser.SetActive(false);
            }
        }
    }


    IEnumerator spawnFood()
    {
        while (score < BossTime) { 
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
