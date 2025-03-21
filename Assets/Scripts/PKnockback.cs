using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKnockback : MonoBehaviour
{
    public float Force;
    public Rigidbody rb;
    public float IFrameTime;
    float Starttime;
    bool Hit;
    PlayerHealth Health;
    AnimationHandler _animHandler;

   void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Starttime = IFrameTime;
        Health = this.GetComponent<PlayerHealth>();
        _animHandler = this.GetComponent<AnimationHandler>();
    }

    void FixedUpdate()
    {
        if (Hit)
        {
            IFrameTime -= Time.deltaTime;
        }
        if(IFrameTime <= 0.0f)
        {
            IFrameTime = Starttime;
            Hit = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //Detects Enemy RS
        if (other.CompareTag("HurtBox"))
        {
            if (!Hit)
            {
                Health.Hit();
                Hit = true;
                if ((rb.transform.position.x - other.transform.position.x) < 0)
                {
                    rb.AddForce(new Vector3(-Force, Force*0.5f, 0));
                }
                else if ((rb.transform.position.x - other.transform.position.x) > 0)
                {
                    rb.AddForce(new Vector3(Force, Force*0.5f, 0));
                }
                _animHandler.KB();
            }
        }
    }

}



