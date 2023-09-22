using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    private static Enemy instance;
    public static Enemy Instance { get { return instance; } }

    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject player;
    [SerializeField] int spawnTime = 2;
    [SerializeField] Transform[] spawnPoints;
    public List<GameObject> Enemies;
    [SerializeField] float shotDistance = 5f;
    public RaycastHit hit;
    [SerializeField] float enemySpeed = 0.5f;
    [SerializeField] GameObject enemy1BulletPrefab;
    [SerializeField] GameObject enemyBoss1BulletPrefab;
    private List<float> shootingCooldowns;
    public List <int> enemyHealthList;
    [SerializeField] float bulletSpeed = 5f;
    public int enemyHealth = 10;
    public GameObject vCamera;
    private bool shouldShowEnemyBoss = false;
    private float coolDownEnemyBoss = 2f;

    
    
    




    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Enemies = new List<GameObject>();
        shootingCooldowns = new List<float>();
        enemyHealthList = new List<int>();
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

        if (levelDesign.Instance.getScore() < levelDesign.Instance.BossTime) { 
        

        for (int i = 0; i < Enemies.Count; i++)
         {
            if (Enemies[i] != null && player !=null)
            {

                // Calculate the direction to the player
                Vector3 directionToPlayer = player.transform.position - Enemies[i].transform.position;
                // Calculate the distance to the player
                float distanceToPlayer = directionToPlayer.magnitude;
                // Calculate the angle in radians
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
                float angleDegrees = angle * Mathf.Rad2Deg;
                Enemies[i].transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
                if (distanceToPlayer < shotDistance)
                {
                    if (shootingCooldowns[i] <= 0f)
                    {
                        Enemies[i].GetComponent<AudioSource>().Play();
                        ShootBullet(Enemies[i].transform.position, enemy1BulletPrefab);
                        shootingCooldowns[i] = 1f;
                    }
                    else
                        shootingCooldowns[i] -= Time.deltaTime;
                }
                else
                {



                    // Calculate the velocity vector
                    Vector3 velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * enemySpeed;

                    // Update the enemy's position based on the calculated velocity
                    Enemies[i].transform.position += velocity * Time.deltaTime;

                    // Rotate the enemy towards the player

                }
            }
            }
        }

        else // Enemy Boss below
        {

            if(player != null) { 
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x);
            float angleDegrees = angle * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angleDegrees-90);

            if (true)
            {
                // Calculate the velocity vector
                Vector3 velocity = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 3f;
                    shakeCam.Instance.shakeCamera();

                    // Update the enemy's position based on the calculated velocity
                    transform.position += velocity * Time.deltaTime;

                if (!shouldShowEnemyBoss)
                    StartCoroutine(ShowEnemyBoss());

                if (coolDownEnemyBoss <= 1f)
                {
                        transform.GetComponent<AudioSource>().Play();
                    for(int i = 0; i < transform.childCount; i++)
                    ShootBullet(transform.GetChild(i).position,enemyBoss1BulletPrefab);
                    coolDownEnemyBoss = 2f;
                }
                else
                    coolDownEnemyBoss -= Time.deltaTime;
            }

        }

    }
    }


    IEnumerator spawnEnemy()
    {
        while (levelDesign.Instance.getScore() < levelDesign.Instance.BossTime)
        {
            if(Enemies.Count < 15) {
                Debug.Log(Enemies.Count);          
            int randomSpawnPoint = Random.Range(0, 4);
            GameObject enemy1;
            if(randomSpawnPoint < 2) 
            {
                float randomY = Random.Range(-10, 10);
                Vector3 enemyPosition = new Vector2(spawnPoints[randomSpawnPoint].position.x, spawnPoints[randomSpawnPoint].position.y + randomY);
                enemy1 = Instantiate(enemyPrefab1, enemyPosition, Quaternion.identity);
            }
            else
            {
                float randomX = Random.Range(-10, 10);
                Vector3 enemyPosition = new Vector2(spawnPoints[randomSpawnPoint].position.x + randomX, spawnPoints[randomSpawnPoint].position.y);
                enemy1 = Instantiate(enemyPrefab1, enemyPosition, Quaternion.identity);
                
            }
            Enemies.Add(enemy1);
            shootingCooldowns.Add(0f);
            enemyHealthList.Add(enemyHealth);

            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void ShootBullet(Vector3 spawnPosition, GameObject bulletPrefab)
    {
        
        // Instantiate a bullet at the enemy's position
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Calculate the direction to the player
        Vector3 direction = (player.transform.position - spawnPosition).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the bullet to face the player
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply velocity to the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(bullet, 3f);
    }


    IEnumerator ShowEnemyBoss()
    {

        foreach (GameObject obj in Enemies)
        {
            Destroy(obj);
        }
        Enemies.Clear();


        Time.timeScale = 0f;
        // Disable the virtual camera's follow.
        vCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;

        // Store the initial position and rotation of the camera.
        Vector3 initialPosition = vCamera.transform.position;
        Quaternion initialRotation = vCamera.transform.rotation;

        Vector3 finalPosition = transform.position;

        float journeyDuration = 2.0f; // Adjust this duration as needed.
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < journeyDuration)
        {
            float journeyFraction = (Time.realtimeSinceStartup - startTime) / journeyDuration;
            // Interpolate between the initial camera position and the enemy boss position.
            vCamera.transform.position = Vector3.Lerp(initialPosition, finalPosition, journeyFraction);

            vCamera.transform.position = new Vector3(vCamera.transform.position.x, vCamera.transform.position.y, -10f);

            yield return null; // Wait for the next frame.
        }

        // Ensure the camera is at the exact position of the enemy boss.
        vCamera.transform.position = finalPosition;

        vCamera.transform.position = new Vector3(vCamera.transform.position.x, vCamera.transform.position.y, -10f);
        shouldShowEnemyBoss = true;

        // Reverse the process to return to the player.
        startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < journeyDuration)
        {
            float journeyFraction = (Time.realtimeSinceStartup - startTime) / journeyDuration;
            // Interpolate between the final position and the player's position.
            vCamera.transform.position = Vector3.Lerp(finalPosition, player.transform.position, journeyFraction);

            vCamera.transform.position = new Vector3(vCamera.transform.position.x, vCamera.transform.position.y, -10f);

            yield return null; // Wait for the next frame.
        }



        // Ensure the camera is at the exact position of the enemy boss.
        vCamera.transform.position = transform.position;

        vCamera.transform.position = new Vector3(vCamera.transform.position.x, vCamera.transform.position.y, -10f);
        shouldShowEnemyBoss = true;
        Time.timeScale = 1f;
        vCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }



}
