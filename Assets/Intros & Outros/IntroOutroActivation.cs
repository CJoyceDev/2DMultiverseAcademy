using System.Collections;
using System.Collections.Generic;
/*using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroOutroActivation : MonoBehaviour
{
    //The images for the comic. The PrevPanel, NewPanel & CurrentPanel variables store the current panel that the player is looking at and the panel before it
    public GameObject Panel1, Panel2, Panel3, CurrentPanel, PrevPanel, NewPanel;

    public Button BackButton;

    //SceneName stores the name of the scene that will be moved into 
    public string SceneName;

    public AudioClip PageTurn;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPanel = Panel1;

        NewPanel = Panel2;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPanel == Panel1)
        {
            BackButton.interactable = false;

            NewPanel = Panel2;
        }
        else if (CurrentPanel == Panel2)
        {
            BackButton.interactable = true;

            NewPanel = Panel3;
        }
        else if (CurrentPanel == Panel3)
        {
            BackButton.interactable = true;
        }
    }

    public void Skip()
    {
        SceneManager.LoadScene("000 Tutorial");
    }

    public void NextPanel()
    {
        SoundHandler.instance.PlaySound(PageTurn, transform, 1f);

        CurrentPanel.gameObject.SetActive(false);

        PrevPanel = CurrentPanel;

        if (CurrentPanel == Panel1)
        {
            CurrentPanel = Panel2;
        }
        else if (CurrentPanel == Panel2)
        {
            CurrentPanel = Panel3;
        }
        else if (CurrentPanel == Panel3)
        {
            SceneManager.LoadScene("000 Tutorial");
        }

        CurrentPanel.SetActive(true);
    }

    public void BackPanel()
    {
        SoundHandler.instance.PlaySound(PageTurn, transform, 1f);

        CurrentPanel.SetActive(false);

        CurrentPanel = PrevPanel;

        CurrentPanel.SetActive(true);

        if (CurrentPanel == Panel1)
        {
            BackButton.interactable = false;
        }
        else if (CurrentPanel == Panel2)
        {
            PrevPanel = Panel1;
        }
        else if (CurrentPanel == Panel3)
        {
            PrevPanel = Panel2;
        }
        else if (CurrentPanel == Panel3)
        {
            PrevPanel = Panel2;
        }
    }
}