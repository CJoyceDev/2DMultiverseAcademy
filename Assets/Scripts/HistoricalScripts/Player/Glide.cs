using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : MonoBehaviour
{

    PlayerController pc;
    [HideInInspector] public bool abilityEnabled = false;
    Vector3 glideFallVector;
    [SerializeField] float glideFall = -.25f;
    [SerializeField] float glideSpeed = 3.5f;
    
    [HideInInspector] public bool isGliding = false; //bool for animations or something //PD

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        //do check get input from pc and do the gliding or something //PD
        //Gonna use the charges for jumping to trigger this
        if (pc.jumpCharges == 0 && !pc.IsMax)
        {

            if (pc.inputActions.Player.Jump.ReadValue<float>() > 0)
            {
                glideFallVector = new Vector3(0f, glideFall, 0f);

                pc.rb.velocity = glideFallVector;
                pc.walkSpeed = glideSpeed;
                isGliding = true;
            }
            else
            {
                isGliding = false;
                pc.walkSpeed = 3.5f;
            }
            

        }
    }
    public void ActivateAbility()
    {
       
    }


}
