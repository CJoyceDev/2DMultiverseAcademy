using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemyLogic : MonoBehaviour
{


    [SerializeField] Vector3 PosTop, PosBottom;
    [SerializeField] float EnemyMoveSpeed;
    Rigidbody rb;

    private bool MoveUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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


        Vector3 movement = new Vector3(0f, distance, 0f);


        Vector3 curPosition = transform.position;
        //This newPosition line needed to stop asset from returning to origin on first itteration CJ
        Vector3 newPosition = transform.position;

        //Below ifs keeps asset between desired points CJ;
        if (curPosition.y <= PosBottom.y)
        {
            MoveUp = true;

        }
        if (curPosition.y >= PosTop.y)
        {
            MoveUp = false;
        }

        if (MoveUp)
        {
            newPosition = curPosition + movement;
        }
        if (!MoveUp)
        {
            newPosition = curPosition - movement;
        }

        //moves the character model CJ
        rb.MovePosition(newPosition);
    }
}
