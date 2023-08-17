using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level;
    public GameObject optionsScreen;
    [SerializeField] private GameObject _mainMenu;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenOptions()
    {
        _mainMenu.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        _mainMenu.SetActive(true);
        optionsScreen.SetActive(false);
    }
}
