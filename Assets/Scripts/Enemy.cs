using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject player;
    [SerializeField] int spawnTime = 2;
    [SerializeField] Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator spawnEnemy()
    {
        while (true)
        {
                        
            int randomSpawnPoint = Random.Range(0, 4);
            if(randomSpawnPoint < 2) 
            {
                float randomY = Random.Range(-10, 10);
                Vector3 enemyPosition = new Vector2(spawnPoints[randomSpawnPoint].position.x, spawnPoints[randomSpawnPoint].position.y + randomY);
                GameObject enemy1 = Instantiate(enemyPrefab1, enemyPosition, Quaternion.identity, transform);
            }
            else
            {
                float randomX = Random.Range(-10, 10);
                Vector3 enemyPosition = new Vector2(spawnPoints[randomSpawnPoint].position.x + randomX, spawnPoints[randomSpawnPoint].position.y);
                GameObject enemy1 = Instantiate(enemyPrefab1, enemyPosition, Quaternion.identity, transform);
            }


            yield return new WaitForSeconds(spawnTime);
        }
    }
}
