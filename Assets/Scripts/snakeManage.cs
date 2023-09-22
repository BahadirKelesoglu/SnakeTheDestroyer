using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
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

    public GameObject pausePanel;
    public GameObject resumeButton;
    public TextMeshProUGUI winOrLoseText;


    public Joystick joystick;
    float GameobjectRotation;



    private bool animationFlag = true;
    private bool loseFlag = true;
    [SerializeField] AudioClip LoseSound;

    


    private void Awake()
    {
        spritesPlayer = Resources.LoadAll<Sprite>("Player");
    }

    void Start()
    {
        
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
        
         

        if (levelDesign.Instance.takenDamage >= 10) { 
            removeSnakePart();
            if(levelDesign.Instance.getScore() > levelDesign.Instance.growPoint && levelDesign.Instance.getScore() < levelDesign.Instance.BossTime) { 
            levelDesign.Instance.addScore(-2);
                transform.GetComponent<snakeAddBehaviour>().growScore += -levelDesign.Instance.growPoint;
            }
            Debug.Log(levelDesign.Instance.takenDamage);
            levelDesign.Instance.takenDamage = 0;
            
        }
        if (snakeBody[0] != null) { 
        if (snakeBody[0].name != "snakeHead")
        {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            resumeButton.SetActive(false);
            winOrLoseText.text = "LOSE";
                if (loseFlag)
                {
                    transform.GetComponent<AudioSource>().clip = LoseSound;
                    transform.GetComponent<AudioSource>().Play();
                    loseFlag = false;
                }


            }
        }
        else
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            resumeButton.SetActive(false);
            winOrLoseText.text = "LOSE";
            if (loseFlag) { 
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
            Destroy(this);
    }

    void snakeMovement()
    {

        snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * speed * Time.deltaTime;

        
         
             snakeBody[0].transform.Rotate(new Vector3 (0, 0, -rotateSpeed * Time.deltaTime * GameobjectRotation));




        // Use keyboard input for rotation if no touch input
        if (Input.GetAxis("Horizontal") != 0)
            snakeBody[0].transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));




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
