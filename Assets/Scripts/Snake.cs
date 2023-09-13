using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{   private Rigidbody2D rb;
    private Vector2 snakePos;
    // Start is called before the first frame update

    private void Awake()
    {
        snakePos = Vector2.zero;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) {
            snakePos.y += 1;
            
        }

        transform.position = snakePos;
    }
}
