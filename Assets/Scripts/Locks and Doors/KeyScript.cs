using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Door;

    private Vector3 vel;
    private Vector3 Offset;
    public bool PickUp;
    public float smoothTime;
    public bool OpenDoor;
    
    // Start is called before the first frame update
    void Start()
    {
        PickUp = false;
        Offset = new Vector3(-0.5f, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PickUp)
        {   
            
            transform.position = Vector3.SmoothDamp(transform.position, Player.transform.position + Offset, ref vel, smoothTime);
        }
        if (OpenDoor)
        {

            transform.position = Vector3.SmoothDamp(transform.position, Door.transform.position, ref vel, smoothTime*0.25f);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PickUp)
        {
            PickUp = true;
        }
        if (other.CompareTag("Door") && PickUp)
        {
            PickUp = false;
            OpenDoor = true;
        }

    }


  

}
