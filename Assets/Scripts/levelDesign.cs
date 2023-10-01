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
    public GameObject runText;


    public GameObject runArrow;
    public GameObject asteroidPrefab;
    private float asteroidTime = 1f;

    private bool arrowSetActiveFlag = false;
    private bool asteroidFlag = false;
    public float spawnTime = 1.0f;
    [SerializeField] int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public float meteorSpeed = 1.0f;
    public int takenDamage = 0;
    public int growPoint = 5;
    public int BossTime = 5;
    public int levelEnemy;
    public float timeCount = 0;
    
    


    private void Awake()
    {
        
        instance = this;
        score = 0;
        GameManager.Instance.adsInOneGame = 0;
    }

    void Start()
    {
        levelEnemy = 5;
        endGameBorder.SetActive(false);
        beginningBorder.SetActive(true);
        blackHole.SetActive(false);
        runArrow.SetActive(false);
        GameManager.Instance.isMenuActive = false;

        foreach (GameObject laser in laserBorders)
        {
            laser.SetActive(true);
        }

        StartCoroutine(spawnFood());
    }

    private void Update()
    {

        timeCount += Time.deltaTime;
        if(SceneManager.GetActiveScene().buildIndex == 1)
        scoreText.text = "Score: " + score + "/60";

        if (SceneManager.GetActiveScene().buildIndex == 2)
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

            runText.SetActive(true);
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
            

            if (!asteroidFlag)
            {
                
                spawnAsteroid();
                asteroidFlag = true;
                asteroidTime = 1f;

            }
            else
            {
                
                asteroidTime -= Time.deltaTime;
                if(asteroidTime < 0) {
                    
                    asteroidFlag = false;
                    
                }
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

    void spawnAsteroid()
    {

        
        float randomx = Random.Range(-90f, -20f);
        float randomy = Random.Range(-20f, 10f);
        float randomVelocityx = Random.Range(-0.5f, 0.5f);
        float randomVelocityy = Random.Range(-0.5f, 0.5f);

        Vector3 spawnPosition = new Vector3(randomx, randomy, 0f);

        GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D meteorRB = meteor.GetComponent<Rigidbody2D>();
        meteorRB.velocity = new Vector2(randomVelocityx, randomVelocityy) * meteorSpeed;
            
          
        
        
    }   

    public void addScore(int scorePoint)
    {
        score += scorePoint;
        GameManager.Instance.coin += scorePoint;

    }

    public int getScore()
    {
        return score;
    }


}
