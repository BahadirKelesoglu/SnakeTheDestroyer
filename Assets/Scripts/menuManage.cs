using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuManage : MonoBehaviour
{
    [SerializeField] GameObject LButton, LPanel;
    // Start is called before the first frame update
    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Level");

        foreach(Sprite sprite in sprites)
        {
            GameObject temp = Instantiate(LButton) as GameObject;
            temp.GetComponent<Image>().sprite = sprite;
            temp.transform.SetParent(LPanel.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
