using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider MainMusicSlider;
    [SerializeField] private Slider GameMusicSlider;
    private bool startFlag = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    public void SetMusicVolume()
    {

        float volumeMain = MainMusicSlider.value;
        

        myMixer.SetFloat("MainMusic", Mathf.Log10(volumeMain)*20);

        
    }

    public void SetGameVolume()
    {

        
        float volumeGame = GameMusicSlider.value;

        
        myMixer.SetFloat("GameMusic", Mathf.Log10(volumeGame) * 20);

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && startFlag)
        {
            MainMusicSlider.gameObject.SetActive(false);
            GameMusicSlider.gameObject.SetActive(false);
            startFlag = false;
        }

        if(Time.timeScale > 0 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            MainMusicSlider.gameObject.SetActive(false);
            GameMusicSlider.gameObject.SetActive(false);
        }
        else if(Time.timeScale <= 0 && GameManager.Instance.isMenuActive)
        {
            MainMusicSlider.gameObject.SetActive(true);
            GameMusicSlider.gameObject.SetActive(true);
        }
    }
}
