using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public int playerStartHealth;
    PlayerPauseUI ppUI;
    private int playerHealth;

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
