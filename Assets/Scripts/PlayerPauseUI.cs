using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPauseUI : MonoBehaviour
{



    int uiElement = 0;

    [SerializeField] GameObject[] uiCanvas;

    private void Start()
    {
        PlayerUI();
    }

    void SwapUi()
    {
        switch (uiElement)
        {

            case 0:
                uiCanvas[0].SetActive(true);
                break;
            case 1:
                uiCanvas[1].SetActive(true);
                break;
            case 2:
                uiCanvas[2].SetActive(true);
                break;
            case 3:
                uiCanvas[3].SetActive(true);
                break;

        }

        for (int i = 0; i < uiCanvas.Length; i++)
        {
            if (uiElement != i)
            {
                uiCanvas[i].SetActive(false);
            }
        }

    }

    public void PlayerUI()
    {
        uiElement = 0;
        SwapUi();
        Time.timeScale = 1;
    }

    public void PauseUI()
    {
        uiElement = 1;
        SwapUi();
        Time.timeScale = 0;
    }

    public void DeathUI()
    {
        uiElement = 2;
        SwapUi();
        Time.timeScale = 0;
    }

    public void WinUI()
    {
        uiElement = 3;
        SwapUi();
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }


}