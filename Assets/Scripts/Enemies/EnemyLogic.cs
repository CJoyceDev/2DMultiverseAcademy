using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] Vector3 PosLeft, PosRight;
    [SerializeField] float EnemyMoveSpeed;
    Rigidbody rb;

    public bool MoveRight;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
    }

    // Fixed update required to stop frame rate affecting movement speed CJ
    void FixedUpdate()
    {
        EnemyPatrol();
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
            transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

        }
        if (curPosition.x >= PosRight.x)
        {
            MoveRight = false;
            transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }

        if (MoveRight) 
        {
            newPosition = curPosition + movement;
            
        }
        if (!MoveRight) 
        {
            newPosition = curPosition - movement;
        }

      
      

        //moves the character model CJ
        rb.MovePosition(newPosition);
    }

    //Removes enemy when shot
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}


