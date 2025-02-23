using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] GameObject ShotPrefab;
    [SerializeField] Transform shootTransform;
    PlayerController pc;
    PlayerShot Shot;
    bool Active = true;
    Rigidbody rb;
    public float Delaytime = 0.1f;
    public bool delay = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Active)
        {
            if (delay) //If the time elapsed is more than the fire rate, allow a shot
            {
                
                if (InputHandler.Ability1Pressed || InputHandler.Ability1Held)
                {
                    Debug.Log("Pin");
                    //StopAllCoroutines();
                    Shot = Instantiate(ShotPrefab, shootTransform.position, Quaternion.identity).GetComponent<PlayerShot>();
                    Shot.Initialize(this, shootTransform);
                   

                delay = false;
                StartCoroutine(Cooldown()); 

                }
                 //set new time of last shot
            }
        }


    }

  

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Delaytime);

        delay = true;
    }

    // For The player to only use ability as evie CD
    public void ActivateAbility()
    {
        Active = true;
    }

    public void DeActivateAbility()
    {
        Active = false;
    }



}
