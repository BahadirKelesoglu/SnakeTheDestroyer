using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtons : MonoBehaviour
{
    public GameObject pausePanel;

    private void Awake()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseButton()
    {


            Time.timeScale = 0f;
            
            pausePanel.SetActive(true);
            GameManager.Instance.isMenuActive = true;
        
    }

    public void MainMenuButton()
    {
        
        SceneManager.LoadScene(0);
    }

    public void ContinueButton()
    {

        Time.timeScale = 1f;       
        pausePanel.SetActive(false);

    }

    public void RestartButton()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
