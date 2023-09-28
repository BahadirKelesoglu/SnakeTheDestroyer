using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Claims;
using TMPro;
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
    public int score;
    int priceOfSkins = 200;
    int levelName = 1;
    // Start is called before the first frame update



    private void Awake()
    {
       

        MPanel.SetActive(false);
        LPanel.SetActive(false);
        buyButton.SetActive(false);       
        //emptySprite = snakeGun.GetComponent<SpriteRenderer>().sprite;
        snakeGun.SetActive(false);



    }
    void Start()
    {


        coinUI.text = "Coins: " + GameManager.Instance.coin;

        Sprite[] sprites = Resources.LoadAll<Sprite>("Level");

        foreach(Sprite sprite in sprites)
        {
            GameObject temp = Instantiate(LButton) as GameObject;
            temp.GetComponent<Image>().sprite = sprite;
            temp.transform.SetParent(LPanelChild.transform, false);
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + levelName.ToString();
            levelName++;
            string sceneName = sprite.name;
            temp.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }

        Sprite[] spritesPlayer = Resources.LoadAll<Sprite>("Player");

        
        foreach (Sprite sprite in spritesPlayer)
        {
            
            GameObject temp = Instantiate(MButton) as GameObject;            
            temp.GetComponent<Image>().sprite = sprite;
            string skinName = temp.GetComponent<Image>().sprite.name;
            if(!PlayerPrefs.HasKey(skinName))
            {
                PlayerPrefs.SetInt(skinName, 0);
            }
            else if(PlayerPrefs.GetInt(skinName) == 1)
            {
                temp.transform.GetChild(0).gameObject.SetActive(false);
            }
            temp.transform.SetParent(MPanelChild.transform, false);
            priceOfSkins += 200;
            temp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = priceOfSkins.ToString();
            temp.GetComponent<Button>().onClick.AddListener(() => selectGun(sprite, skinName));            

        }
        
        if(GameManager.Instance.activeSkin != 99) { 
        snakeGun.SetActive(true);
        snakeGun.GetComponent<Image>().sprite = spritesPlayer[GameManager.Instance.activeSkin];
        }
    }

    // Update is called once per frame
    void Update()
    {


        coinUI.text = "Coins: " + GameManager.Instance.coin;
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

    public void EndlessMode()
    {
        SceneManager.LoadScene("Level Endless");
    }

    public void selectGun(Sprite sprite,string i)
    {
        if (snakeGun.GetComponent<Image>().sprite != sprite) {
            snakeGun.SetActive(true);
            snakeGun.GetComponent<Image>().sprite = sprite;
            int activeSkin = int.Parse(i);
            GameManager.Instance.activeSkin = activeSkin;


            buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
            if (PlayerPrefs.GetInt(i) == 0) {
                buyButton.SetActive(true);
                //Sorry for that.There is a logic writing style in the buy func.--- It just control our coin for do we have enough to buy it.
                if (GameManager.Instance.coin >= int.Parse(MPanelChild.transform.GetChild(int.Parse(i)).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text))
                    buyButton.GetComponent<Button>().onClick.AddListener(() => buy(i));
                
            
                
            }
            else 
                buyButton.SetActive(false);

            
        }
        else
        {
            
            GameManager.Instance.activeSkin = 99;
            
            buyButton.GetComponent<Button>().onClick.RemoveListener(() => buy(i));
            buyButton.SetActive(false);
            snakeGun.GetComponent<Image>().sprite = null;
            snakeGun.SetActive(false);
        }

        GameManager.Instance.Save();
        


    }
    

    public void buy(string skinIndex)
    {
        PlayerPrefs.SetInt(skinIndex, 1);
        int IntSkinIndex = int.Parse(skinIndex);
        buyButton.SetActive(false) ;
        
            GameObject skinObject = MPanelChild.transform.GetChild(IntSkinIndex).gameObject;
            
            GameObject childOfParent = skinObject.transform.GetChild(0).GetChild(0).gameObject;
            
            skinObject.transform.GetChild(0).gameObject.SetActive(false);
            GameManager.Instance.coin -= int.Parse(childOfParent.GetComponent<TextMeshProUGUI>().text);
            

            GameManager.Instance.Save();
    }

    public void Adbuy()
    {
        PlayerPrefs.SetInt("0", 1);
        int IntSkinIndex = int.Parse("0");
        buyButton.SetActive(false);

        GameObject skinObject = MPanelChild.transform.GetChild(IntSkinIndex).gameObject;
        
        GameObject childOfParent = skinObject.transform.GetChild(0).GetChild(0).gameObject;
        
        skinObject.transform.GetChild(0).gameObject.SetActive(false);
        //GameManager.Instance.coin -= int.Parse(childOfParent.GetComponent<TextMeshProUGUI>().text);


        GameManager.Instance.Save();

        RewardedAdContoller.Instance.LoadRewardedAd();
        RewardedAdContoller.Instance.ShowRewardedAd();
    }


}
