using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Claims;
using TMPro;
using Unity.VisualScripting;
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
        emptySprite = snakeGun.GetComponent<SpriteRenderer>().sprite;
        



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
            temp.transform.SetParent(MPanelChild.transform, false);
            temp.GetComponent<Button>().onClick.AddListener(() => selectGun(sprite, skinName));            

        }
        Debug.Log(GameManager.Instance.activeSkin);
        if(GameManager.Instance.activeSkin != 99)
        snakeGun.GetComponent<SpriteRenderer>().sprite = spritesPlayer[GameManager.Instance.activeSkin];
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

    public void selectGun(Sprite sprite,string i)
    {
        if (snakeGun.GetComponent<SpriteRenderer>().sprite != sprite) { 
            snakeGun.GetComponent<SpriteRenderer>().sprite = sprite;
            int activeSkin = int.Parse(i);
            GameManager.Instance.activeSkin = activeSkin;


            buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
            if (PlayerPrefs.GetInt(i) == 0) { 
            buyButton.GetComponent<Button>().onClick.AddListener(() => buy(i));
            buyButton.SetActive(true);
                
            }
            else
                buyButton.SetActive(false);
        }
        else
        {
            snakeGun.GetComponent<SpriteRenderer>().sprite = emptySprite;
            GameManager.Instance.activeSkin = 99;
            
            buyButton.GetComponent<Button>().onClick.RemoveListener(() => buy(i));
            buyButton.SetActive(false);
        }

        GameManager.Instance.Save();
        //Debug.Log(GameManager.Instance.activeSkin);


    }
    

    public void buy(string skinIndex)
    {
        PlayerPrefs.SetInt(skinIndex, 1);
        int IntSkinIndex = int.Parse(skinIndex);
        buyButton.SetActive(false) ;
        
            GameObject skinObject = MPanelChild.transform.GetChild(IntSkinIndex).gameObject;
            Debug.Log(skinObject.name);
            GameObject childOfParent = skinObject.transform.GetChild(0).GetChild(0).gameObject;
            Debug.Log(childOfParent.GetComponent<TextMeshProUGUI>().text);
            GameManager.Instance.coin -= int.Parse(childOfParent.GetComponent<TextMeshProUGUI>().text);
            GameManager.Instance.Save();
        
    }


}
