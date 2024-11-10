using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyFowardLogic : MonoBehaviour
{


    [SerializeField] float JumpPower;
    public GameObject Jumppoint1;
    float JumpDistance1;
    float JumpHeight1;
    bool Jump1;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Jump1 = true;
        
        rb = GetComponent<Rigidbody>();
        JumpDistance1 = Jumppoint1.transform.position.x - rb.transform.position.x;

        JumpHeight1 = Jumppoint1.transform.position.y - transform.position.y + JumpPower;

        Jump();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void Jump()
    {






        rb.AddForce(new Vector3(JumpDistance1, JumpHeight1, 0), ForceMode.Impulse);
      
       
        StartCoroutine(JumpWait());
    }

    IEnumerator JumpWait()
    {

        yield return new WaitForSeconds(3.0f);
        Jump();

    }



}
