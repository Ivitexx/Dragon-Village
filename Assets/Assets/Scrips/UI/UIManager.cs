using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image deadScreen;
    public float fadeSpeed;
    public bool toFade;
    public bool fromFade;
    public float transparence;
    public bool toFadeEndGame;

    [Header("---------INTERFACE---------")]
    public GameObject[] Hearts;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI coinTextWin;
    public GameObject key;

    [Header("---------MENU---------")]
    public GameObject pauseScreen;
    public GameObject optionsScreen;
    public GameObject yourCoins;
    public string mainMenu;

    public Slider musicVolSlider, sfxVolSlider;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        transparence = deadScreen.color.a;

        if(!toFadeEndGame)
        {
            if (toFade)
            {
                deadScreen.color = new Color(deadScreen.color.r, deadScreen.color.g, deadScreen.color.b, Mathf.MoveTowards(deadScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

                if (deadScreen.color.a == 1f)
                {
                    toFade = false;
                }
            }

            if (fromFade)
            {
                deadScreen.color = new Color(deadScreen.color.r, deadScreen.color.g, deadScreen.color.b, Mathf.MoveTowards(deadScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

                if (deadScreen.color.a == 0f)
                {
                    fromFade = false;
                }
            }
        }
        else
        {
            deadScreen.color = new Color(deadScreen.color.r, deadScreen.color.g, deadScreen.color.b, Mathf.MoveTowards(deadScreen.color.a, 1f, 5 * Time.deltaTime));

            if (deadScreen.color.a == 1f)
            {
                toFade = false;
            }
        }
    }


    public void Resume()
    {
        GameManager.instance.PauseUnpase();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
