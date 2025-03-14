using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMobileUI : MonoBehaviour
{
    [SerializeField] GameObject mUI;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetFloat("toggleMUI") == 0)
        {
            mUI.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("toggleMUI") == 1)
        {
            mUI.SetActive(true);
        }
    }

}

    
