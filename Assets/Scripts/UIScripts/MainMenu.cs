using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    //button functions, starts level index 0 in the build, the other is uselss actually, it's a browser game //PD
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void SG2()
    {
        SceneManager.LoadScene(2);
    }

    

    public void SG3()
    {
        SceneManager.LoadScene(3);
    }
    public void SG4()
    {
        SceneManager.LoadScene(4);
    }
    public void SG5()
    {
        SceneManager.LoadScene(5);
    }

    /*public void ExitGame()
    {
        Application.Quit();
    }*/
}
