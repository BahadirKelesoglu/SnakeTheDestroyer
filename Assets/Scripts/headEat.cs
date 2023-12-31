﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;

public class headEat : MonoBehaviour
{
    public int simplefood = 1;
    public GameObject MenuPanel;
    public GameObject ContinueButton;
    public TextMeshProUGUI winOrLoseText;
    [SerializeField] AudioSource getDamage;
    [SerializeField] AudioClip WinSound;
    private float stopTimer=1f;
    private bool finishedFlag = true;
    


    private void Start()
    {
        // Initialize the Google Mobile Ads SDK (call this once at the start).
        Interstitial.InitializeInterstitial();
        Interstitial.LoadLoadInterstitialAd();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            
            
            Destroy(other.gameObject);
            levelDesign.Instance.addScore(simplefood);
            GameManager.Instance.Save();
        }

        if (other.gameObject.CompareTag("wall"))
        {
            
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z+180f);
        }

        if (other.gameObject.CompareTag("enemy1Bullet"))
        {
            levelDesign.Instance.takenDamage += 1;
            Destroy(other.gameObject);
            getDamage.Play();
            shakeCam.Instance.shakeCamera();
        }

        if (other.gameObject.CompareTag("EnemyBossBullet"))
        {
            levelDesign.Instance.takenDamage += 5;
            Destroy(other.gameObject);
            getDamage.Play();
            shakeCam.Instance.shakeCamera();
        }

        if (other.gameObject.CompareTag("Enemy1"))
        {
            levelDesign.Instance.takenDamage += 5;
            Destroy(other.gameObject);
            getDamage.Play();
        }

        if (other.gameObject.CompareTag("EnemyBoss"))
        {
            Destroy(transform.gameObject);
            
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            transform.parent.GetComponent<Animator>().SetBool("Finish", true);
            stopTimer = 0f;

        }

       
    }

    private void Update()
    {

        if(stopTimer <= 0)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= -3f && finishedFlag)
            {
                MenuPanel.SetActive(true);
                ContinueButton.SetActive(false);
                Time.timeScale = 0f;
                winOrLoseText.text = "WIN";
                getDamage.clip = WinSound;
                getDamage.Play();
                finishedFlag = false;

                

            }
            if(stopTimer <= -3f && Time.timeScale ==1)
                Time.timeScale = 0f;
            }

    }
}
