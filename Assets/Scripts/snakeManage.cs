using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class snakeManage : MonoBehaviour
{

    [SerializeField] float speed = 250f;
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> snakeBody = new List<GameObject>();



    void Start()
    {

        // Create the initial snake with a head and tail.
        CreateInitialSnake();
    }
    private void Update()
    {

        // Check for key press to add a body part.
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddBodyPartBetweenHeadAndTail();
        }
    }


    void FixedUpdate()
    {

        snakeMovement();
    }

    void snakeMovement()
    {
        // Store the initial position of the tail.
        Vector3 tailPosition = snakeBody[snakeBody.Count - 1].transform.position;

        snakeBody[0].GetComponent<Rigidbody2D>().velocity = snakeBody[0].transform.right * speed * Time.deltaTime;
        if(Input.GetAxis("Horizontal") != 0)
            snakeBody[0].transform.Rotate(new Vector3 (0, 0, -rotateSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));
        
        if(snakeBody.Count > 1) //Every part take the position of the next part, thanks to that, they follow next part
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

    /*  void CreateBodyParts()
      {

          if(snakeBody.Count == 0)
          {
              GameObject head = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
              snakeBody.Add(head);
              bodyParts.RemoveAt(0);
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


              bodyParts.RemoveAt(0);

              part.GetComponent<snakeObject>().clearSnakeList();
              countup = 0;

          }


      }*/

    void CreateInitialSnake()
    {

        // Create the initial snake with a head and tail.
        GameObject head = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
        snakeBody.Add(head);
        bodyParts.RemoveAt(0);

        GameObject tail = Instantiate(bodyParts[0], transform.position - transform.right, transform.rotation, transform);
        snakeBody.Add(tail);
        bodyParts.RemoveAt(0);
        snakeObject snakeO = head.GetComponent<snakeObject>();
        snakeO.UpdateSnakeList();
        snakeObject snake1 = tail.GetComponent<snakeObject>();
        snake1.UpdateSnakeList();
    }

    void AddBodyPartBetweenHeadAndTail()
    {
        if (bodyParts.Count > 0 && snakeBody.Count >= 2)
        {
            // Get references to the head, tail, and the new body part.
            GameObject head = snakeBody[0];
            GameObject tail = snakeBody[snakeBody.Count - 1];
            GameObject newBodyPart = bodyParts[0];

            // Calculate the position for the new body part between the head and tail.
            Vector3 newPosition = (head.transform.position + tail.transform.position) / 2; // ALGILAMIYOR!!!

            // Create the new body part and insert it into the snakeBody list.
            GameObject part = Instantiate(newBodyPart, newPosition, head.transform.rotation, transform);
            snakeBody.Insert(1, part);

            // Remove the used body part from the bodyParts list.
            bodyParts.RemoveAt(0);

            // Update the snakeList of the new body part.
            snakeObject snake2 = part.GetComponent<snakeObject>();
            snake2.UpdateSnakeList();
        }
    }
}
