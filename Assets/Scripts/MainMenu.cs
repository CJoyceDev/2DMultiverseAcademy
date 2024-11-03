using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    //button functions, starts level index 0 in the build, the other is uselss actually, it's a browser game //PD
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    /*public void ExitGame()
    {
        Application.Quit();
    }*/
}
