using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : MonoBehaviour
{

    PlayerController pc;
    public bool abilityEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (abilityEnabled)
        {
            //do check get input from pc and do the gliding or something


        }
    }

  

}
