using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BuzzsawScript : MonoBehaviour


{

    [SerializeField] Vector3 PosLeft, PosRight;
    [SerializeField] float EnemyMoveSpeed;
    [SerializeField] bool notStationary = true;
    [SerializeField] ParticleSystem leftSparks;
    [SerializeField] ParticleSystem rightSparks;

    [SerializeField] AudioClip BuzzSound;

    Rigidbody rb;
    bool canToggle = true;

    public bool MoveRight;

    private float rotationSpeed = 500f;

    public Quaternion startRotatinLeft;
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        if (notStationary)
        {
            Move();
        }
        else
        {
            leftSparks.Play();
            rightSparks.Stop();
        }

        CheckSparks();
        /*
        if (transform.position.x == PosLeft.x)
        {
            Debug.Log("Left");
            SoundHandler.instance.PlaySound(BuzzSound, transform, 1f);
        }

        if (transform.position.x == PosRight.x - 3)
        {
            Debug.Log("Right");
            SoundHandler.instance.PlaySound(BuzzSound, transform, 1f);
        }*/
    }

    private void Move()
    {
        //gets the distance the model can move perframe CJ
        float distance = EnemyMoveSpeed * Time.deltaTime;
        /*float hAxis = Input.GetAxis("Horizontal");*/


        Vector3 movement = new Vector3(distance, 0f, 0f);


        Vector3 curPosition = transform.position;
        //This newPosition line needed to stop asset from returning to origin on first itteration CJ
        Vector3 newPosition = transform.position;

        //Below ifs keeps asset between desired points CJ;
        if (curPosition.x <= PosLeft.x)
        {
            MoveRight = true;

            if (notStationary)
            {
                SoundHandler.instance.PlaySound(BuzzSound, transform, 0.25f);
            }

            //transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            leftSparks.Stop();
            rightSparks.Stop();

        }
        if (curPosition.x >= PosRight.x)
        {
            MoveRight = false;

            if (notStationary)
            {
                SoundHandler.instance.PlaySound(BuzzSound, transform, 0.25f);
            }

            //transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }

        if (MoveRight)
        {
            newPosition = curPosition + movement;
        }
        if (!MoveRight)
        {
            newPosition = curPosition - movement;
        }

        transform.position = newPosition;
    }


    void CheckSparks() //Need to stop effect early to stop it playing while moving wrong way CJ
    {

        Vector3 curPosition = transform.position;
        if (curPosition.x <= PosLeft.x + 0.5)
        {
           
           
            leftSparks.Stop();

            rightSparks.Play();

        }
        if (curPosition.x >= PosRight.x - 0.5)
        {
     
            leftSparks.Play();

            rightSparks.Stop();

         
        }

    }
}
