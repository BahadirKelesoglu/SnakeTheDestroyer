using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeObject : MonoBehaviour
{
    public class snakePos
    {
        public Vector2 position;
        public Quaternion rotation;

        public snakePos(Vector2 pos, Quaternion rot) { 
            position = pos; 
            rotation = rot; 
        }
    }

   public List<snakePos> snakeList = new List<snakePos>();
    void Start()
    {
        //I have created the class for instead of using 2 list of transform and rotation, make a class like gameobject to hold their pos and rot
        // I will add this code to every body parts of the snake. So this will help me to body parts follow each other continiously.

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateSnakeList();
    }

    public void UpdateSnakeList()
    {
        snakeList.Add(new snakePos(transform.position, transform.rotation));
    }

    public void clearSnakeList()
    {
        snakeList.Clear();
        snakeList.Add(new snakePos(transform.position, transform.rotation));
    }
}
