using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtons : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject FirstGamePanel;

    

    private void Awake()
    {
        pausePanel.SetActive(false);


    }

    private void Start()
    {


        if (PlayerPrefs.HasKey("isFirstGame"))
        {
            if (PlayerPrefs.GetInt("isFirstGame") == 0)
            {
                Time.timeScale = 0f;
                FirstGamePanel.SetActive(true);
            }

            else
                Time.timeScale = 1f;

        }
        else
        {
            Time.timeScale = 0f;
            FirstGamePanel.SetActive(true);
            
        }
    }



    public void PauseButton()
    {


            Time.timeScale = 0f;
            
            pausePanel.SetActive(true);
            GameManager.Instance.isMenuActive = true;
        
    }

    public void MainMenuButton()
    {
        // I WOULD LIKE TO SHOW ADS WHEN PRESSED THAT BUTTON AND LATER WILL GO TO MAIN MENU
        //SceneManager.LoadScene(0);
        Interstitial.ShowInterstitialAd();
        GameManager.Instance.mainMenuAds = true;
        GameManager.Instance.adsInOneGame = 1;
    }

    public void ContinueButton()
    {

        Time.timeScale = 1f;       
        pausePanel.SetActive(false);

    }

    public void RestartButton()
    {
        // I WOULD LIKE TO SHOW ADS WHEN PRESSED THAT BUTTON AND LATER WILL RESTART THE GAME
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Interstitial.ShowInterstitialAd();
        GameManager.Instance.restartFlagAds = true;
        GameManager.Instance.adsInOneGame = 1;
    }

    public void FirstGame()
    {
        Time.timeScale = 1; 
        FirstGamePanel.SetActive(false);
        PlayerPrefs.SetInt("isFirstGame", 1);
    }
}
