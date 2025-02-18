using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScript : MonoBehaviour
{
    public bool Move;
    private bool _Move = false;
    private Vector3 MoveSpot;
    public bool DESTROY;
    public Vector3 MoveValue;

    public float smoothTime;
    private Vector3 vel = Vector3.zero;

    public void Activate()
    {

        if (Move){
            MoveSpot = this.transform.position + MoveValue;
            _Move = true;
        }


        if (DESTROY)
        {
            Destroy(gameObject);
        }

    }

    void FixedUpdate()
    {
        if (_Move) { 
        this.transform.position = Vector3.SmoothDamp(this.transform.position, MoveSpot, ref vel, smoothTime);
        }
    
    }
}
