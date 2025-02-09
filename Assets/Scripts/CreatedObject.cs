using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedObject : MonoBehaviour
{
    //RS
    public float LifeTime;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, LifeTime);//Destroy the object after the LifeTime has passed
    }

    public void OnCollisionEnter(Collision collision)
    {
        //destroys the object when an enemy collides 
        if (collision.gameObject.tag == "KillBox")
        {
            Destroy(this.gameObject);
        }
    }
}
