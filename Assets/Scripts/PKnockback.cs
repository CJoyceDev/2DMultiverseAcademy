using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKnockback : MonoBehaviour
{
    public float Force;
    public Rigidbody rb;
    public float IFrameTime;
    float Starttime;
    public static bool IFrameActive;
    PlayerHealth Health;
    AnimationHandler _animHandler;

   void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        IFrameActive = false; // fixes the iframes being stuck on when you die CJ
        Starttime = IFrameTime;
        Health = this.GetComponent<PlayerHealth>();
        _animHandler = this.GetComponent<AnimationHandler>();

    }

    void FixedUpdate()
    {

       

    }

    void OnTriggerEnter(Collider other)
    {
        //Detects Enemy RS
        if (other.CompareTag("HurtBox"))
        {
            if (!IFrameActive)
            {
                Health.Hit();
                IFrameActive = true;
                StartCoroutine(Iframes());
                if ((rb.transform.position.x - other.transform.position.x) < 0)
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(new Vector3(-Force, Force*0.3f, 0), ForceMode.Impulse);
                }
                else if ((rb.transform.position.x - other.transform.position.x) > 0)
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(new Vector3(Force, Force*0.3f, 0), ForceMode.Impulse);
                }
                _animHandler.KB();
            }
        }
    }



    private IEnumerator Iframes()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(IFrameTime);

        // Set the invulnerable flag to false
        IFrameActive = false;
    }
}





