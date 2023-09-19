using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeAddBehaviour : MonoBehaviour
{
    public GameObject part1;
    snakeManage sm;
    private int growScore = 0;

     
    private void Start()
    {
        sm = GetComponent<snakeManage>();
    }
    // Update is called once per frame
    void Update()
    {
        if (levelDesign.Instance.getScore() >= growScore)
        {        
                sm.addBodyPart(part1);
            growScore += 5;
        }

    }
}
