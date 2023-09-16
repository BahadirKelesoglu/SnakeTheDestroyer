using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public int coin = 0, numberOfSkins = 0, activeSkin=99;

    private void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("numberOfSkins") && PlayerPrefs.HasKey("activeSkin"))
            
        {
            coin = PlayerPrefs.GetInt("coin");
            numberOfSkins = PlayerPrefs.GetInt("numberOfSkins");
            activeSkin = PlayerPrefs.GetInt("activeSkin");
        }
        else
            Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("numberOfSkins", numberOfSkins);
        PlayerPrefs.SetInt("activeSkin", activeSkin);
    }
}
