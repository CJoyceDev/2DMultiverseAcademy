using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroOutroActivation : MonoBehaviour
{
    //The images for the comic. The PrevPanel & CurrentPanel variables store the current panel that the player is looking at and the panel before it
    public GameObject Panel1, Panel2, Panel3, Panel4, CurrentPanel, PrevPanel, NewPanel;

    public Button BackButton;

    //SceneName controls the scene that it will move into 
    public string SceneName;

    public AudioClip PageTurn;

    //AS = AudioSource
    AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPanel = Panel1;

        NewPanel = Panel2;

        AS = GetComponent<AudioSource>();

        AS.clip = PageTurn;
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

            NewPanel = Panel4;
        }
        else if (CurrentPanel == Panel4)
        {
            BackButton.interactable = true;
        }
    }

    public void Skip()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void NextPanel()
    {
        AS.Play();

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
            CurrentPanel = Panel4;
        }
        else if (CurrentPanel == Panel4)
        {
            SceneManager.LoadScene(SceneName);
        }

        CurrentPanel.SetActive(true);
    }

    public void BackPanel()
    {
        AS.Play();

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
        else if (CurrentPanel == Panel4)
        {
            PrevPanel = Panel4;
        }
    }
}