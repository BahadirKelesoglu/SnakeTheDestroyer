using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject player;
    [SerializeField] int spawnTime = 2;
    [SerializeField] Transform[] spawnPoints;
    private List<GameObject> Enemies;
    [SerializeField] float shotDistance = 5f;
    public RaycastHit hit;
    [SerializeField] float enemySpeed = 0.5f;
    [SerializeField] GameObject enemy1BulletPrefab;
    private List<float> shootingCooldowns;
    [SerializeField] float bulletSpeed = 5f;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        Enemies = new List<GameObject>();
        shootingCooldowns = new List<float>();
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        

        for (int i = 0; i < Enemies.Count; i++)
         {
            if (Enemies[i] != null)
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
                        ShootBullet(Enemies[i].transform.position);
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


    IEnumerator spawnEnemy()
    {
        while (Enemies.Count < 15)
        {
                        
            int randomSpawnPoint = Random.Range(0, 4);
            GameObject enemy1;
            if(randomSpawnPoint < 2) 
            {
                float randomY = Random.Range(-10, 10);
                Vector3 enemyPosition = new Vector2(spawnPoints[randomSpawnPoint].position.x, spawnPoints[randomSpawnPoint].position.y + randomY);
                enemy1 = Instantiate(enemyPrefab1, enemyPosition, Quaternion.identity, transform);
            }
            else
            {
                float randomX = Random.Range(-10, 10);
                Vector3 enemyPosition = new Vector2(spawnPoints[randomSpawnPoint].position.x + randomX, spawnPoints[randomSpawnPoint].position.y);
                enemy1 = Instantiate(enemyPrefab1, enemyPosition, Quaternion.identity, transform);
                
            }
            Enemies.Add(enemy1);
            shootingCooldowns.Add(0f);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    void ShootBullet(Vector3 spawnPosition)
    {
        // Instantiate a bullet at the enemy's position
        GameObject bullet = Instantiate(enemy1BulletPrefab, spawnPosition, Quaternion.identity);

        // Calculate the direction to the player
        Vector3 direction = (player.transform.position - spawnPosition).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the bullet to face the player
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply velocity to the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(bullet, 3f);
    }

}
