using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class snakeManage : MonoBehaviour
{
    float countup = 0f;
    private float speed = 180f;
    public float scoreSpeed = 1;
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> snakeBody = new List<GameObject>();
    public float partDistance = 0.08f;
    Sprite[] spritesPlayer;
    public GameObject snakeHead;
    private int tailOrderSize = 2;

    public GameObject pausePanel;
    public GameObject resumeButton;
    public TextMeshProUGUI winOrLoseText;


    public Joystick joystick;
    float GameobjectRotation;
    float HorizontalInput;



    private bool animationFlag = true;
    private bool loseFlag = true;
    [SerializeField] AudioClip LoseSound;
    public TextMeshProUGUI deneme;

    


    private void Awake()
    {
        spritesPlayer = Resources.LoadAll<Sprite>("Player");
    }

    void Start()
    {
        animationFlag = true;
        loseFlag = true;
        // Initialize the Google Mobile Ads SDK (call this once at the start).
        Interstitial.InitializeInterstitial();

        CreateBodyParts();
        
    }
    void FixedUpdate()
    {
        
        manageSnakeBody();
        
        snakeMovement();
    }

    private void Update()
    {


        if (transform.GetComponent<Animator>().GetBool("Finish") && animationFlag)
        {
            transform.GetComponent<AudioSource>().Play();
            animationFlag = false;
        }


        GameobjectRotation = joystick.Horizontal;
        HorizontalInput = Input.GetAxis("Horizontal");



        if (levelDesign.Instance.takenDamage >= 2) {
            if (transform.childCount == 1)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            else
            {


                removeSnakePart();
                if (levelDesign.Instance.getScore() > levelDesign.Instance.growPoint && levelDesign.Instance.getScore() < levelDesign.Instance.BossTime && SceneManager.GetActiveScene().name != "Level Endless") {
                    levelDesign.Instance.addScore(-levelDesign.Instance.growPoint);
                    transform.GetComponent<snakeAddBehaviour>().growScore += -levelDesign.Instance.growPoint;
                }
            }

            levelDesign.Instance.takenDamage = 0;

        }

        if (snakeBody != null) { 

        if (snakeBody[0].name != "snakeHead" || snakeBody == null || snakeBody[0].layer != 7)
        {

            if (Time.timeScale == 1)
                Time.timeScale = 0f;


            if (loseFlag)
            {
                Interstitial.LoadLoadInterstitialAd();
                Interstitial.ShowInterstitialAd();
                
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                resumeButton.SetActive(false);
                winOrLoseText.text = "LOSE";
                // Load and show the interstitial ad when needed.

                transform.GetComponent<AudioSource>().clip = LoseSound;
                transform.GetComponent<AudioSource>().Play();
                loseFlag = false;

            }


        }

    }
          else
          {
              if(Time.timeScale == 1)
              Time.timeScale = 0f;


              if (loseFlag) {

                  Interstitial.LoadLoadInterstitialAd();
                  Interstitial.ShowInterstitialAd();
                  
                  Time.timeScale = 0f;
                  pausePanel.SetActive(true);
                  resumeButton.SetActive(false);
                  winOrLoseText.text = "LOSE";

                  transform.GetComponent<AudioSource>().clip = LoseSound;
              transform.GetComponent<AudioSource>().Play();
                  loseFlag = false;

              }


          }


        if (Time.timeScale == 0)
        {
            joystick.gameObject.SetActive(false);
        }
        else
            joystick.gameObject.SetActive(true);
    }

    void manageSnakeBody()
    {
        if (bodyParts.Count > 0)
        {
            CreateBodyParts();
        }

        for (int i = 0; i < snakeBody.Count; i++) {

            if (snakeBody[i] == null)
            {
                snakeBody.RemoveAt(i);
                i -= 1;
            }       
        }
        if (snakeBody.Count == 0)
            snakeBody = null;
    }

    void snakeMovement()
    {

        snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * speed * Time.deltaTime;



        
        if (HorizontalInput != 0)
            snakeBody[0].transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime * HorizontalInput));
        else
        snakeBody[0].transform.Rotate(new Vector3 (0, 0, -rotateSpeed * Time.deltaTime * GameobjectRotation));




        // Use keyboard input for rotation if no touch input





        if (snakeBody.Count > 1) //Every part take the position of the next part, thanks to that, they follow next part
        { 
            for( int i = 1; i<snakeBody.Count; i++ )
            {
                snakeObject snakeO = snakeBody[i-1].GetComponent<snakeObject>();
                snakeBody[i].transform.position = snakeO.snakeList[0].position;
                snakeBody[i].transform.rotation = snakeO.snakeList[0].rotation;
                snakeO.snakeList.RemoveAt(0);
                

            }        
        }
    }

      void CreateBodyParts()
      {

          if(snakeBody.Count == 0)
          {
              GameObject head = snakeHead;
              snakeBody.Add(head);
              
          }

          snakeObject snakeO = snakeBody[snakeBody.Count - 1].GetComponent<snakeObject>();
          if(countup == 0)
          {
              snakeO.clearSnakeList();
          }

          countup += Time.deltaTime;
          if(countup >= partDistance)
          {
              GameObject part = Instantiate(bodyParts[0], snakeO.snakeList[0].position, snakeO.snakeList[0].rotation,transform);
              snakeBody.Add(part);
            part.GetComponent<SpriteRenderer>().sortingOrder = tailOrderSize;
            tailOrderSize++;
              if(snakeBody.Count % 4 == 0 && snakeBody.Count >1) 
              {
                int activeSkin = GameManager.Instance.activeSkin;
                if(PlayerPrefs.GetInt(activeSkin.ToString()) == 1)
                part.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spritesPlayer[activeSkin];
              }

              bodyParts.RemoveAt(0);

              part.GetComponent<snakeObject>().clearSnakeList();
              countup = 0;

          }


      }

      public void addBodyPart(GameObject part)
      {
        bodyParts.Add(part);
      }

    public void removeSnakePart() 
    {
        int lastIndex = snakeBody.Count - 1;
        Destroy(snakeBody[lastIndex]);
    }
}
