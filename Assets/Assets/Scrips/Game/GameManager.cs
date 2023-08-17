using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private Vector3 respawnPosition;

    public string level;

    public int currentCoins;

    private void Awake()
    {
        instance = this;    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpase();
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        HealthManager.instance.currentHealth = 0;

        StartCoroutine(RespawnWaiter());
    }

    public IEnumerator RespawnWaiter()
    {
        UIManager.instance.toFade = true;

        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.CinemachineBrain.enabled = false;

        yield return new WaitForSeconds(0.5f);

        UIManager.instance.fromFade = true;

        PlayerController.instance.transform.position = respawnPosition;
        PlayerController.instance.gameObject.SetActive(true);
        CameraController.instance.CinemachineBrain.enabled = true;
        HealthManager.instance.ResetHealth();
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
        UIManager.instance.coinTextWin.text = "" + currentCoins;
    }

    public void PauseUnpase()
    {
        if(UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void FinishTheGame()
    {
        StartCoroutine(FinishTheGameWaiter());
    }
    public IEnumerator FinishTheGameWaiter()
    {
        UIManager.instance.toFadeEndGame = true;

        yield return new WaitForSeconds(1f);

        UIManager.instance.yourCoins.SetActive(true);

        yield return new WaitForSeconds(5f);

        UIManager.instance.MainMenu();

    }
}
