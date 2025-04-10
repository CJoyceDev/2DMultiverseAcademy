using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] GameObject eraaserAnim;

    Collider collider;

    [SerializeField] Transform WinCamTransform;

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

        collider = GetComponent<Collider>();
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HurtBox"))
        {
            if (!PKnockback.IFrameActive)
            {
                SoundHandler.instance.PlaySound(_player.DamageSound, transform, 0.55f);
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
            SoundHandler.instance.PlaySound(_player.CoinSound, transform, 0.4f);
        }

        if (other.CompareTag("EndGoal"))
        {
            collider.enabled = false;
            _player.hasWon = true;
            StartCoroutine(ReactivateCollider(0.7f));

        }

        if (other.CompareTag("Checkpoint"))
        {
            /*_player.PlaySound(_player.CheckpointSound);*/
            SoundHandler.instance.PlaySound(_player.CheckpointSound, transform, 0.5f);
            // lastCheckpointPos = transform.position;
            // touchedCheckpoint = true;
            // Debug.Log("Checkpoint Reached: " + lastCheckpointPos);

        }

        if (other.CompareTag("EndCamZone"))
        {

            Transform winCamPos = other.transform.Find("WinCamPos");

            if (winCamPos != null)
            {
                cammeraManager.targetTransform = winCamPos;
                cammeraManager.winOffset = 10;
                cammeraManager.inWinZone = true;
               
            }


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

        if (other.CompareTag("EndCamZone"))
        {
            cammeraManager.inWinZone = false;
            cammeraManager.facingRight = !cammeraManager.facingRight;
            cammeraManager.CallTurn();
            
           
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
        //shieldPS.Play();
        SoundHandler.instance.PlaySound(_player.PortalSound, transform, 0.1f, 2.5f);
        GameObject effectsInstance = Instantiate(eraaserAnim, transform.position + new Vector3(0, 0.4f, -1.3f), Quaternion.Euler(0, 0, 0));
        Destroy(effectsInstance, 0.8f);
        while (elapsedTime < animTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerModels.SetActive(false);

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ppUI.DeathAnimUI();
    }

    //Plays the teleport effect and disables the player models CJ
    IEnumerator DeathPuff()
    {
        float elapsedTime = 0f;
        //deathPS.Play();

        //while (elapsedTime < animTime)
        //{
        //    shieldPS.Stop();
        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}
        //shieldPS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

     

        playerModels.SetActive(false);
       

        //This is required to give the models a chance to dissapear before the menu appears CJ
        while (elapsedTime < animTime + 0.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        ppUI.DeathAnimUI();
    }

    IEnumerator ReactivateCollider(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Win();
    }

    //      if (inputSystem != null)
    //    {
    //        inputSystem.SetActive(false);
    //        StartCoroutine(SpawnSequence());
    ////GameObject effectsInstance = Instantiate(spawnPS, transform.position + new Vector3(0, -playerHeight / 2, 0), Quaternion.Euler(90, 0, 0));
    //GameObject effectsInstance = Instantiate(spawnPS, transform.position + new Vector3(0, 0.4f, -1.3f), Quaternion.Euler(0, 0, 0));
    //Destroy(effectsInstance, 1f);

    //animSystem.enabled = false;
    //        //portalImage.SetActive(true);
    //    }
}
