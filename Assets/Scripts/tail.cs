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
        Vector3 directionToNearestEnemy = gun.position - nearestEnemy;
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
                ShootBullet(nearestEnemy);
                shootEffect.Play();
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
        else
            animator.SetBool("isFired", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy1Bullet"))
        {
            levelDesign.Instance.takenDamage += 1;
            Destroy(other.gameObject);
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

    private void ShootBullet(Vector3 nearestEnemy)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.GetChild(0).GetChild(0).position, Quaternion.identity, transform.GetChild(0).GetChild(0));

        Vector3 direction = (nearestEnemy - transform.GetChild(0).GetChild(0).position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        if(nearestEnemy == null)
            Destroy(bullet);

        

        Destroy(bullet, 1f);
    }

    }
