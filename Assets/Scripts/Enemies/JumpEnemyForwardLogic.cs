using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyFowardLogic : MonoBehaviour
{


    [SerializeField] float JumpPower;
    public GameObject Jumppoint1;
    float JumpDistance1;
    float JumpHeight1;
    //bool Jump1;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       // Jump1 = true;
        //gets the objects rigidbody and the distance and hight it will jump each time CD
        rb = GetComponent<Rigidbody>();
        JumpDistance1 = Jumppoint1.transform.position.x - rb.transform.position.x;

        JumpHeight1 = Jumppoint1.transform.position.y - transform.position.y + JumpPower;

        Jump();
    }


    void Jump()
    {
        // adds force to make the enemy jump in a direction
        rb.AddForce(new Vector3(JumpDistance1, JumpHeight1, 0), ForceMode.Impulse);
        StartCoroutine(JumpWait());
    }

    IEnumerator JumpWait()
    {
        // the delay between each jump CD
        yield return new WaitForSeconds(3.0f);
        Jump();

    }



}
