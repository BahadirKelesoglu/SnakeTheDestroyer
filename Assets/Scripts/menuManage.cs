using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManage : MonoBehaviour
{
    [SerializeField] GameObject LButton, LPanelChild, LPanel,MPanel;
    // Start is called before the first frame update

    private void Awake()
    {
        MPanel.SetActive(false);
        LPanel.SetActive(false);
    }
    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Level");

        foreach(Sprite sprite in sprites)
        {
            GameObject temp = Instantiate(LButton) as GameObject;
            temp.GetComponent<Image>().sprite = sprite;
            temp.transform.SetParent(LPanelChild.transform, false);
            string sceneName = sprite.name;
            temp.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLevelPanel()
    {   
        LPanel.SetActive(!LPanel.activeSelf);
    }

    public void OpenMarketPanel()
    {
        MPanel.SetActive(!MPanel.activeSelf);
    }
}
