using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroOutroSpawning : MonoBehaviour
{
    public GameObject IntroPanels, OutroPanels;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(IntroPanels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OutroActivate()
    {
        Instantiate(OutroPanels);
    }
}
