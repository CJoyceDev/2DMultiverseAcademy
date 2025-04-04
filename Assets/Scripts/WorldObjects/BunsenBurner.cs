using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BunsenBurner : MonoBehaviour
{

    bool isFireOn = true;
    bool isFirePlaying = false;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] ParticleSystem fireEffect;
    [SerializeField] float fireDelay;
    bool isHurtActive = true; //need a separate bool for effect and hurt box CJ

    public AudioClip FireSound;
    
    bool isSparkOn = false;
    bool isSparking = false;
    [SerializeField] ParticleSystem sparkEffect;
    public AudioClip SparkSound;
    float sparkDelay;

    // Start is called before the first frame update
    void Start()
    {
        //boxCollider = GetComponent<BoxCollider>(); 
        sparkDelay = fireDelay - 1f;
        
        StartCoroutine(SwitchFireOnOff());
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(isFireOn)
        {
            CreateFire();
        }
        else
        {
            StopFire();
        }

        if(isHurtActive)
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
      
        if (isSparkOn)
        {
            CreateSpark();
        }
        else
        {
            StopSpark();
        }


    }

    private IEnumerator SwitchFireOnOff()
    {
        while(true){
            yield return new WaitForSeconds(fireDelay);
            isFireOn = !isFireOn;
            isHurtActive = !isHurtActive;
            if (!isFireOn)
            {
                StartCoroutine(SwitchSparkOnOff());
            }
        }
    }

    void CreateFire()
    {
        if (!isFirePlaying)
        {
            fireEffect.Play();
            SoundHandler.instance.PlaySound(FireSound, transform, 0.3f, 3);
            isFirePlaying = true;
           
        }

    }

    void StopFire()
    {
        fireEffect.Stop();
        isFirePlaying = false;

    }

    private IEnumerator SwitchSparkOnOff()
    {
        
            yield return new WaitForSeconds(sparkDelay);
            isSparkOn = !isSparkOn;
        

    }


    void CreateSpark()
    {
        if (!isSparking)
        {
            sparkEffect.Play();
            SoundHandler.instance.PlaySound(SparkSound, transform, 0.3f, 1);
            
            isSparking = true;

        }

    }

    void StopSpark()
    {
        sparkEffect.Stop();
        isSparking = false;
        isSparkOn = !isSparkOn;
    }

}
