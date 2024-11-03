using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPauseUI : MonoBehaviour
{


    //Just some Index value to use as the swapping on the switch //PD
    int uiElement = 0;
    //array for ui canvas elements holding the ui menus/windows //PD
    [SerializeField] GameObject[] uiCanvas;

    //run once to default to player UI on level start //PD
    private void Start()
    {
        PlayerUI();
    }

    //changes Shown UI by activating or diactivating the objects/canvases //PD
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

    //the lot below changes the index for the corresponding ui in the array holding the canvas objects, some also pause the game run time/time scale //PD
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

    //some functions for the buttons, no reason to fear //PD
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
