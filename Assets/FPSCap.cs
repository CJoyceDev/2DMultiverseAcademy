using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCap : MonoBehaviour
{
    public static FPSCap instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    [SerializeField] int frameCap;
    int i = 0;


    // Update is called once per frame
    void FixedUpdate()
    {
        
        i++;
        if (i >= 20)
        {
            Application.targetFrameRate = frameCap;
            i = 0;
        }

        
    }
}
