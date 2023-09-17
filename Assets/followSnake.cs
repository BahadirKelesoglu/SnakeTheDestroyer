using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followSnake : MonoBehaviour
{
    public GameObject snakeManager;
    private Transform follow;
    // Start is called before the first frame update
    void Start()
    {
        follow = snakeManager.transform.GetChild(0);
        GetComponent<CinemachineVirtualCamera>().Follow = follow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
