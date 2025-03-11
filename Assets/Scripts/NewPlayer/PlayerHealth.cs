using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public int playerStartHealth;
    PlayerPauseUI ppUI;
    private int playerHealth;

    [SerializeField] HeartSpriteScript heartSpriteScript;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerStartHealth;
        ppUI = GameObject.FindGameObjectWithTag("CameraEmpty").GetComponent<PlayerPauseUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth == 0)
        {
            print("kill");
            ppUI.DeathAnimUI();
            playerHealth = 3;

        }

        if (heartSpriteScript != null)
        {
            heartSpriteScript.healthAmount = playerHealth;
        }
        else
        {
            //Haha I made an error log, stop forgeting to attach this or im gonn have an infinite while loop somewhere to nuke this //PD
            Debug.LogError("Player Health Script missing heart sprite refrence, Camera>Canvas>MobileControlls>Hearts , Drag it into Player Health Heart Sprite Script Slot On the Player Object");
        }

    }

    public void Hit()
    { 
        playerHealth = playerHealth - 1;
    }

    void Heal()
    {
        playerHealth = playerHealth + 1;
    }
}
