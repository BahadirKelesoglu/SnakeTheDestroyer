using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeAddBehaviour : MonoBehaviour
{
    public GameObject part1;
    snakeManage sm;
    public int growScore;

     
    private void Start()
    {
        growScore = 1;
        sm = GetComponent<snakeManage>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (levelDesign.Instance.getScore() >= growScore)
        {        
                sm.addBodyPart(part1);
                growScore = levelDesign.Instance.getScore()+1;
        }

    }
}
