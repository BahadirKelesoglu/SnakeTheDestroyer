using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public int coin = 1500, numberOfSkins = 0, activeSkin=99;
    public int levelScore;
    private void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        if (PlayerPrefs.HasKey("numberOfSkins"))
            
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
