using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPauseUI : MonoBehaviour
{


    //Just some Index value to use as the swapping on the switch //PD
    int uiElement = 0;
    //array for ui canvas elements holding the ui menus/windows //PD
    [SerializeField] GameObject[] uiCanvas;
    PlayerController pc;

    //Button Sprites
    [SerializeField] Image button1, button2, button3, button4;


    private void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //run once to default to player UI on level start //PD
    private void Start()
    {
        PlayerUI();
    }

    private void Update()
    {
        if (InputHandler.PauseButtonPressed)
        {
            if (uiElement == 0)
            {
                PauseUI();
            }
            else if (uiElement == 1)
            {
                ResumeGame();
            }
        }
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
            case 4:
                uiCanvas[4].SetActive(true);
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

    public void DeathAnimUI()
    {
        uiElement = 4;
        SwapUi();
        StartCoroutine(PlayDeathAnimation());
    }



    //some functions for the buttons, no reason to fear //PD
    public void RestartLevel()
    {

        //Play some SOund

        //Animate Button Press and transition


        Time.timeScale = 1;
        PlayerController.Checkpoint = default;
        PlayerController.coinsSaved = 0;
        CoinsScript.collectedCoins.Clear();
        CoinsScript.collectedSaved.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {

        //Play some SOund

        //Animate Button Press and transition

        Time.timeScale = 1;
        PlayerController.Checkpoint = default;
        PlayerController.coinsSaved = 0;
        CoinsScript.collectedCoins.Clear();
        CoinsScript.collectedSaved.Clear();
        SceneManager.LoadScene(0);
    }

    public void LoadCheckpoint()
    {

        //Play some SOund

        //Animate Button Press and transition

        Time.timeScale = 1;
        PlayerUI();
        CoinsScript.collectedCoins.Clear();
        CoinsScript.collectedCoins = CoinsScript.collectedSaved;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ResumeGame()
    {
        //Play some SOund

        //Animate Button Press and transition


        PlayerUI();
    }


    //Transiton Stuff, gonna add

    void FadeOutGeneral()
    {
        //play some animation
        //scene change
        //play other animation

        //or so that's the idea, prob will play fade in on level load, not here
    }

    void FadeInGeneral()
    {

    }

    void FadeOutMenuClose()
    {

    }

    void FadeInMenuClose()
    {

    }

    /*IEnumerator ButtonColorFade(Image x)
    {
        x.color = new Color(0.9f,0.9f,0.9f,1);

        yield return new WaitForSeconds(0.5f);

        x.color = new Color(1, 1, 1, 1);
    }*/

    /*    public void ResumeGame()
        {
            //Play some SOund

            //Animate Button Press and transition
            StartCoroutine(ResumeGame2());

        }
    */
    //Silly me, i forgot couritines don't work with time scale 0 D:, dang /PD

    /*IEnumerator ResumeGame2()
    {
        //Play some SOund

        //Animate Button Press and transition
        button1.color = new Color(0.9f, 0.9f, 0.9f, 1);

        yield return new WaitForSeconds(0.5f);

        button1.color = new Color(1, 1, 1, 1);

        PlayerUI();
    }*/


    private IEnumerator PlayDeathAnimation()
    {



        // Wait until the animation finishes
        yield return new WaitForSecondsRealtime(0.34f);

        // Now switch to the actual Death UI
        DeathUI();
    }
}

