using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float LifeTime = 2;

    private void OnCollisionEnter(Collision other)
    {
        //Detects Enemy RS
        if (other.gameObject.tag == "KillBox")
        {
            Debug.Log("Enemy Hit");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        //Detects Player so that it doesn't block them after it appears RS
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Player Hit");
        }
    }

    private void Update()
    {
        Destroy(this.gameObject, LifeTime);//Removes Attack Collider if it hasn't collided with anything RS
    }
}
