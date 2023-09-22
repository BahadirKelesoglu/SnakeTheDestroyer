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
        growScore = levelDesign.Instance.growPoint;
        sm = GetComponent<snakeManage>();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (levelDesign.Instance.getScore() >= growScore)
        {        
                sm.addBodyPart(part1);
                growScore = levelDesign.Instance.getScore()+ levelDesign.Instance.growPoint;
        }

    }
}
