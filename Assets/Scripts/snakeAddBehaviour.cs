using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeAddBehaviour : MonoBehaviour
{
    public GameObject part1;
    snakeManage sm;
    // Start is called before the first frame update
    private void Start()
    {
        sm = GetComponent<snakeManage>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {        
                sm.addBodyPart(part1);   
        }

    }
}
