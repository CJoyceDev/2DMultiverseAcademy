using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerColliderTrigger : MonoBehaviour
{
    Rigidbody rb;
    PlayerPauseUI ppUI;
   [SerializeField] CamFollowManager cammeraManager;
    [SerializeField] CJMovementWithRB _player;

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

        if (other.CompareTag("LookDownZone"))
        {
           
            _player.inLookDownZone = true;
            cammeraManager.currentYOffset = -3;
            cammeraManager.CallLookDown();
        }
        else
        {
            _player.inLookDownZone = false;
        }


        void Kill()
        {
            print("kill");
            ppUI.DeathAnimUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("LookDownZone"))
        {
            print("look down zne left ");
            _player.inLookDownZone = false;
            cammeraManager.currentYOffset = 2;
        }
    }

    void Win()
    {
        print("Win");
        /*Spawn();*/
        ppUI.WinAnimUI();
    }
}
