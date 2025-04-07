using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnemyScript : MonoBehaviour {


    [SerializeField] Vector3 PosLeft, PosRight;
    [SerializeField] float EnemyMoveSpeed;
    [SerializeField] float StunTime;
    [SerializeField] bool notStationary = true;
    [SerializeField] GameObject Hurtbox;
    [SerializeField] GameObject StuckPin;


    bool Stun;
    bool StationaryEnemy = false;
    MeshRenderer mr;
    Rigidbody rb;
    Collider bc;
    bool canToggle = true;

    public bool MoveRight;

    CJMovementWithRB cj;
    public AudioClip DeathSound;

    // Start is called before the first frame update
    void Start()
    {
        cj = GameObject.FindGameObjectWithTag("Player").GetComponent<CJMovementWithRB>();
        rb = GetComponent<Rigidbody>();
        bc = Hurtbox.GetComponent<Collider>();
        mr = StuckPin.GetComponent<MeshRenderer>();
        Stun = false;

        if (!notStationary)
        {
            StationaryEnemy = true;
        }
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
    }

    // Fixed update required to stop frame rate affecting movement speed CJ
    void FixedUpdate()
    {
        if (notStationary)
        {
            EnemyPatrol();
        }
    }


    void EnemyPatrol()
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
            //transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

        }
        if (curPosition.x >= PosRight.x)
        {
            MoveRight = false;
            //transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }

        if (MoveRight)
        {
            newPosition = curPosition + movement;
            transform.rotation = Quaternion.LookRotation(movement);

        }
        if (!MoveRight)
        {
            newPosition = curPosition - movement;
            transform.rotation = Quaternion.LookRotation(-1 * movement);
        }




        //moves the character model CJ
        rb.MovePosition(newPosition);
    }

    //Removes enemy when shot
    void OnTriggerEnter(Collider other)
    {
        if (!Stun) { 
        if (other.CompareTag("Bullet"))
        {
            /*cj.PlaySound(cj.EnemyDeathSound);*/
            SoundHandler.instance.PlaySound(cj.EnemyDeathSound, transform, 1f);
            //Destroy(this.gameObject);
            Destroy(other.gameObject);
                if (!StationaryEnemy)
                {
                    notStationary = false;
                }

            transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            rb.isKinematic = false;
            bc.enabled = false;
            mr.enabled = true;
            Stun = true;
            StartCoroutine(Stunned());
        }
        }

        if (other.CompareTag("IgnoredByEnemy") && canToggle)
        {
            Toggle();

            canToggle = false;
        }

        if (other.CompareTag("KillBox"))
        {
            Object.Destroy(this.gameObject);
        }
    }

    void Toggle()
    {
        if (MoveRight == false)
        {
            MoveRight = true;
        }
        else
        {
            MoveRight = false;
        }
        StartCoroutine(ToggleCoolDown());
    }

    IEnumerator ToggleCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canToggle = true;
    }

    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(StunTime);
        rb.isKinematic = false;
        bc.enabled = true;
        mr.enabled = false;
        Stun = false;
        if (!StationaryEnemy) {
            
            notStationary = true;
            }
        else { 
        transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
            }
    }


}




