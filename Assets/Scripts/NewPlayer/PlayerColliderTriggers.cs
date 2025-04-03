using System;
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
   public bool touchedCheckpoint = false;
    [SerializeField] ParticleSystem damagePS;
    [SerializeField] ParticleSystem shieldPS;
    [SerializeField] ParticleSystem deathPS;
    [SerializeField] GameObject playerModels;
    


    public Vector3 lastCheckpointPos;
    [SerializeField] float animTime = 1; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ppUI = GameObject.FindGameObjectWithTag("CameraEmpty").GetComponent<PlayerPauseUI>();
        lastCheckpointPos = transform.position;
        damagePS.Stop();
        if (shieldPS != null)
        {
          shieldPS.Stop();
        }

        if (deathPS != null)
        {
            deathPS.Stop();
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HurtBox"))
        {
            if (!PKnockback.IFrameActive)
            {
                SoundHandler.instance.PlaySound(_player.DamageSound, transform, 1f);
                damagePS.Play();
            }
            //DamagePlayer();
            /*_player.PlaySound(_player.DamageSound);*/
            
        }

        if (other.CompareTag("KillBox"))
        {
            Kill();
        }

        if (other.CompareTag("Finish"))
        {
            cammeraManager.winOffset = 5;
            cammeraManager.inWinZone = true;
           
            print("win");
            //Win();
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

        if (other.CompareTag("Coin"))
        {
            /*_player.PlaySound(_player.CoinSound);*/
            SoundHandler.instance.PlaySound(_player.CoinSound, transform, 1f);
        }

        if (other.CompareTag("Checkpoint"))
        {
            /*_player.PlaySound(_player.CheckpointSound);*/
            SoundHandler.instance.PlaySound(_player.CheckpointSound, transform, 1f);
           // lastCheckpointPos = transform.position;
           // touchedCheckpoint = true;
           // Debug.Log("Checkpoint Reached: " + lastCheckpointPos);
            
        }

    }


    public void Kill()
    {
        print("kill");

        
        StartCoroutine(DeathSequence());
       

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
        if (CoinStore.CoinStorage != null)
        {
            CoinStore.CoinStorage.LevelEndSet();
        }
        print("Win");
        /*Spawn();*/
        ppUI.WinAnimUI();
    }

    //Stops the player moving and plays the shield particle effect. CJ
    IEnumerator DeathSequence()
    {
        
        rb.constraints = RigidbodyConstraints.FreezeAll;
        float elapsedTime = 0f;
        shieldPS.Play();
        
        while (elapsedTime < animTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
     
        StartCoroutine(DeathPuff());
    }

    //Plays the teleport effect and disables the player models CJ
    IEnumerator DeathPuff()
    {
        float elapsedTime = 0f;
        deathPS.Play();
       
        while (elapsedTime < animTime)
        {
            shieldPS.Stop();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        shieldPS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        playerModels.SetActive(false);
       

        //This is required to give the models a chance to dissapear before the menu appears CJ
        while (elapsedTime < animTime + 0.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        ppUI.DeathAnimUI();
    }
}
