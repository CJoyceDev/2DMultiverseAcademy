using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroOutroActivation : MonoBehaviour
{
    public float LifeTime1, LifeTime2, LifeTime3, LifeTime4;

    public GameObject Panel1, Panel2, Panel3, Panel4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(Panel1.gameObject, LifeTime1);
        Destroy(Panel2.gameObject, LifeTime2);
        Destroy(Panel3.gameObject, LifeTime3);
        Destroy(Panel4.gameObject, LifeTime4);
    }
}
