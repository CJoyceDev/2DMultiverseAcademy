using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] MeshRenderer mr, mr2;
    [SerializeField] BoxCollider bc, bc2;
    [SerializeField] float timeToCrumble, recoverTime;

    
    public GameObject testTubeLeft;
    public GameObject testTubeRight;

    [SerializeField] BreakingAnim breakingAnim;
    [SerializeField] BoxCollider hitBox;

    [SerializeField] AudioClip BreakSound;

    [SerializeField] ParticleSystem BreakPrarticles;
    [SerializeField] GameObject ParticleSpawn;


    //start timer
    public void StartCrumbling()
    {
        Debug.Log("A");
        StartCoroutine(Crumble(timeToCrumble));
    }

    //at the end of the timer, hide the platform rendered and coliders, start timer to restart them //PD
    IEnumerator Crumble(float time)
    {
        float timeTotal = 0;

        SoundHandler.instance.PlaySound(BreakSound, transform, 0.4f, timeToCrumble);

        if (ParticleSpawn != null && BreakPrarticles != null)
        {
            Instantiate(BreakPrarticles, ParticleSpawn.transform.position, ParticleSpawn.transform.rotation);
        }
        else { Debug.Log("Missing Spawnpoint/Partticle Effect"); }

        while (timeTotal < time)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
        Debug.Log("B");
        
        if (breakingAnim == null)
        {

            print(null);
        }
        else
        {
            
            breakingAnim.isBreaking = true;
        }
        (bc.enabled, bc2.enabled) = (false, false);
        if (breakingAnim == null)
        { (mr.enabled, mr2.enabled) = (false, false); }


        if (testTubeLeft != null)
        {

            ShakeScript shakeScript = testTubeLeft.GetComponent<ShakeScript>();

            if (shakeScript != null)
            {
                shakeScript.isActivated = false;
            }
        }

        if (testTubeRight != null)
        {

            ShakeScript shakeScript = testTubeRight.GetComponent<ShakeScript>();

            if (shakeScript != null)
            {
                shakeScript.isActivated = false;
            }
        }

        StartCoroutine(ResetPlatform(recoverTime));



    }
    //Restarts the platform to be visible and colidable after the timer //PD
    IEnumerator ResetPlatform(float time)
    {
        float timeTotal = 0;

        while (timeTotal < time)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
        Debug.Log("C");

        (mr.enabled, mr2.enabled) = (true, true);
        (bc.enabled, bc2.enabled) = (true, true);

        if (breakingAnim != null)
        {
            breakingAnim.ResetPos(breakingAnim.testTubeLeft);
            breakingAnim.ResetPos(breakingAnim.testTubeRight);
        }

      
        if (breakingAnim != null)
        {
             breakingAnim.isBreaking = false;
        }


        hitBox.enabled = true;




    }


}
