using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
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
            //do stuff


        }
    }
    public void ActivateAbility()
    {

    }
}
