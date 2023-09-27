using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tail : MonoBehaviour
{
    
    List<GameObject> enemyList;
    private Transform gun;
    public GameObject bulletPrefab;
    [SerializeField] float shootWait = 0.8f;
    [SerializeField] float bulletSpeed = 10f;
    private ParticleSystem shootEffect;
    private Animator animator;
    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioClip shootSoundClip;
    [SerializeField] AudioClip takenDamageClip;


    [SerializeField] Transform gun1SpawnPoint;
    [SerializeField] Transform gun2SpawnPoint1;
    [SerializeField] Transform gun2SpawnPoint2;
    [SerializeField] Transform gun3SpawnPoint1;
    [SerializeField] Transform gun3SpawnPoint2;
    [SerializeField] Transform gun3SpawnPoint3;

    private void Awake()
    {
        shootEffect = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Start()
    {

        enemyList = Enemy.Instance.Enemies;
        gun = transform.GetChild(0);
        
    }

    private void Update()
    {
        enemyList = Enemy.Instance.Enemies;
        Vector3 nearestEnemy = FindNearestEnemy();
        if(transform.GetChild(0).GetComponent<SpriteRenderer>().sprite != null) {
            // Calculate the direction to the player
            string spriteName = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
            Vector3 directionToNearestEnemy;
            if (nearestEnemy != Vector3.zero) { 
               directionToNearestEnemy = gun.position - nearestEnemy;
        // Calculate the distance to the player
        float distanceToNearestEnemy = directionToNearestEnemy.magnitude;
        // Calculate the angle in radians
        float angle = Mathf.Atan2(directionToNearestEnemy.y, directionToNearestEnemy.x);
        float angleDegrees = angle * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0, 0, angleDegrees+90);
        if(distanceToNearestEnemy < 8)
        {
            if(shootWait >= 0.8f) 
            {
                if(levelDesign.Instance.getScore() < levelDesign.Instance.BossTime) {
                            switch (spriteName)
                            {
                                case "0":
                                    ShootBullet(nearestEnemy, gun1SpawnPoint.position);
                                    break;
                                case "1":
                                    ShootBullet(nearestEnemy, gun2SpawnPoint1.position,gun2SpawnPoint2.position);
                                    break;
                                case "2":
                                    ShootBullet(nearestEnemy, gun3SpawnPoint1.position, gun3SpawnPoint2.position, gun3SpawnPoint3.position);
                                    break;

                                default:
                                    break;

                            }
                //ShootBullet(nearestEnemy, gun1SpawnPoint);
                shootEffect.Play();
                shootSound.clip = shootSoundClip;
                shootSound.Play();
                animator.SetBool("isFired", true);
                shootWait = 0f;
                    }
                }
                else { 
                shootWait += Time.deltaTime;
                animator.SetBool("isFired", false);
                }
            }
            }
        }
        else
            animator.SetBool("isFired", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy1Bullet"))
        {
            levelDesign.Instance.takenDamage += 1;
            Destroy(other.gameObject);

            shootSound.clip = takenDamageClip;
            shootSound.Play();
            shakeCam.Instance.shakeCamera();

        }

        if (other.gameObject.CompareTag("EnemyBossBullet"))
        {
            levelDesign.Instance.takenDamage += 5;
            Destroy(other.gameObject);

            shootSound.clip = takenDamageClip;
            shootSound.Play();
            shakeCam.Instance.shakeCamera();

        }


    }


    private Vector3 FindNearestEnemy()
    {
        float nearestDistance = float.MaxValue;
        Vector3 nearestPosition = Vector3.zero;

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] != null)
            {
                float distance = Vector3.Distance(gun.position, enemyList[i].transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestPosition = enemyList[i].transform.position;
                }
            }
        }
        return nearestPosition;
    }

    private void ShootBullet(Vector3 nearestEnemy, Vector3 spawnPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        Vector3 direction = (nearestEnemy - transform.GetChild(0).GetChild(0).position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        if(nearestEnemy == null)
            Destroy(bullet);

        

        Destroy(bullet, 1f);
    }

    private void ShootBullet(Vector3 nearestEnemy, Vector3 spawnPosition1, Vector3 spawnPosition2)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition1, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, spawnPosition2, Quaternion.identity);

        Vector3 direction = (nearestEnemy - transform.GetChild(0).GetChild(0).position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet2.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        



        Destroy(bullet, 1f);
        Destroy(bullet2, 1f);

    }

    private void ShootBullet(Vector3 nearestEnemy, Vector3 spawnPosition1, Vector3 spawnPosition2, Vector3 spawnPosition3)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition1, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, spawnPosition2, Quaternion.identity);
        GameObject bullet3 = Instantiate(bulletPrefab, spawnPosition3, Quaternion.identity);

        Vector3 direction = (nearestEnemy - transform.GetChild(0).GetChild(0).position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet2.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet3.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bullet3.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;




        Destroy(bullet, 1f);
        Destroy(bullet2, 1f);
        Destroy(bullet3, 1f);

    }

}
