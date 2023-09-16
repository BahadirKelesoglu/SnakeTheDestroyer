using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Claims;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class menuManage : MonoBehaviour
{
    private int coins;
    public TMP_Text coinUI;
    [SerializeField] GameObject LButton, LPanelChild, LPanel,MPanel, MPanelChild, MButton, snakeGun,buyButton;
    private Sprite emptySprite;
    // Start is called before the first frame update

    private void Awake()
    {
        MPanel.SetActive(false);
        LPanel.SetActive(false);
        buyButton.SetActive(false);
        coinUI.text = "Coins: " + coins.ToString();
        emptySprite = snakeGun.GetComponent<SpriteRenderer>().sprite;
        
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

        Sprite[] spritesPlayer = Resources.LoadAll<Sprite>("Player");

        foreach (Sprite sprite in spritesPlayer)
        {
            GameObject temp = Instantiate(MButton) as GameObject;
            temp.GetComponent<Image>().sprite = sprite;
            temp.transform.SetParent(MPanelChild.transform, false);
            temp.GetComponent<Button>().onClick.AddListener(() => selectGun(sprite));

        }
        //snakeGun.GetComponent<SpriteRenderer>().sprite = spritesPlayer[0];
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

    public void addCoin(int value) {

        coins += value;
        coinUI.text = "Coins: " + coins.ToString();

    }

    public void selectGun(Sprite sprite)
    {
        if (snakeGun.GetComponent<SpriteRenderer>().sprite != sprite) { 
            snakeGun.GetComponent<SpriteRenderer>().sprite = sprite;
            buyButton.SetActive(true);
        }
        else
        {
            snakeGun.GetComponent<SpriteRenderer>().sprite = emptySprite;
            buyButton.SetActive(false);
        }
     
        
        
    }
}
