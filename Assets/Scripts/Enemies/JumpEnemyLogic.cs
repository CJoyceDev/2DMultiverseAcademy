using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyLogic : MonoBehaviour
{

  
    [SerializeField] float JumpPower;
    public GameObject Jumppoint1;
    float JumpDistance1;
    float JumpHeight1;
    bool Jump1;
    bool Jump2;
   // bool IsGrounded;
    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //gets the objects rigidbody and the distance and hight it will jump each time CD
        Jump1 = true;
        Jump2 = false;
        rb = GetComponent<Rigidbody>();
        JumpDistance1 = Jumppoint1.transform.position.x - rb.transform.position.x;
      
        JumpHeight1 = Jumppoint1.transform.position.y - transform.position.y + JumpPower;
      
        Jump();
    }

    void Jump()
    {
          
       
        // checks weather its jumping forward or back then adds the approprate force CD
        if (Jump1)
        {
            rb.AddForce(new Vector3(JumpDistance1, JumpHeight1, 0), ForceMode.Impulse);
            Jump1 = false;
            Jump2 = true;
        }
        else if (Jump2)
        {
            rb.AddForce(new Vector3(-JumpDistance1, JumpHeight1, 0), ForceMode.Impulse);
            Jump1 = true;
            Jump2 = false;
             
        }
       StartCoroutine(JumpWait());
    }

    IEnumerator JumpWait()
    {
        // the delay between each jump CD
        yield return new WaitForSeconds(3.0f);
        Jump();

    }



}
