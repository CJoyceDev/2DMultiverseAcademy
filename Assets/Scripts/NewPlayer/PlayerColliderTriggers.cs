using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerColliderTrigger : MonoBehaviour
{
    Rigidbody rb;
    PlayerPauseUI ppUI;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ppUI = GameObject.FindGameObjectWithTag("CameraEmpty").GetComponent<PlayerPauseUI>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HurtBox"))
        {
            //DamagePlayer();
        }

        if (other.CompareTag("KillBox"))
        {
            Kill();
        }

        if (other.CompareTag("Finish"))
        {
            Win();
        }

        if (other.CompareTag("NonOOBKillBox"))
        {
            Kill();
        }


        void Kill()
        {
            print("kill");
            ppUI.DeathUI();
        }
    }

    void Win()
    {
        print("Win");
        /*Spawn();*/
        ppUI.WinUI();
    }
}
