using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float LifeTime = 2;

    public static float ForceX = 1.5f;
    public static float ForceY = 0f;

    public bool FacingLeft = false;

    GameObject Player;

    [SerializeField] AudioSource Source;
    [SerializeField] AudioClip PushSound;

    PlayerController PlayerController;

    Rigidbody rb;
    Vector3 PushForce = new Vector3(ForceX, ForceY, 0);

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
    }
    private void OnCollisionEnter(Collision other)
    {
        //Detects Enemy RS
       if (other.gameObject.tag == "HurtBox")
        {
            rb = other.gameObject.GetComponent<Rigidbody>();

            //plays the push sound effect when the attack is activated
            PlayerController.playSound(PushSound);

            if (FacingLeft == false)
            {
                Debug.Log("Push Right");
                rb.AddForce(PushForce, ForceMode.Impulse);
            }
            if (FacingLeft == true)
            {
                Debug.Log("Push Left");
                rb.AddForce(-PushForce, ForceMode.Impulse);
            }

            Debug.Log("Enemy Hit");
            Destroy(this);
        }

        //Detects Player so that it doesn't block them after it appears RS
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Player Hit");
        }

        if (other.gameObject.tag == "Box")
        {
            rb = other.gameObject.GetComponent<Rigidbody>();

            //plays the push sound effect when the attack is activated
            PlayerController.playSound(PushSound);           
            
            if (FacingLeft == false)
            {
                Debug.Log("Push Right");
                rb.AddForce(PushForce, ForceMode.Impulse);
            }
            if (FacingLeft == true)
            {
                Debug.Log("Push Left");
                rb.AddForce(-PushForce, ForceMode.Impulse);
            }
            

            Destroy(this);
        }
    }

    private void Update()
    {
        Destroy(this.gameObject, LifeTime);//Removes Attack Collider if it hasn't collided with anything RS
    }
}
