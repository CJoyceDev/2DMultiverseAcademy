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

    // Start is called before the first frame update
    void Start()
    {
        //boxCollider = GetComponent<BoxCollider>(); 
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

    }

    private IEnumerator SwitchFireOnOff()
    {
        while(true){
            yield return new WaitForSeconds(fireDelay);
            isFireOn = !isFireOn;
            isHurtActive = !isHurtActive;
        }
    }

    void CreateFire()
    {
        if (!isFirePlaying)
        {
            fireEffect.Play();
            isFirePlaying = true;
        }

    }

    void StopFire()
    {
        fireEffect.Stop();
        isFirePlaying = false;

    }
}
