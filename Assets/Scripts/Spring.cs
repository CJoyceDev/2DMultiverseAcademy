using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    public SpringJoint springJoint;
    public float ResetValue;
    bool Connect;
    bool Return;
    public GameObject player;
    Vector3 Startpos;
    Rigidbody rb;
    Rigidbody Prb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Startpos = this.transform.position;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        ResetValue = springJoint.maxDistance;
        Return = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Connect)
        {
            //this.gameObject.transform.position = player.transform.position;
            player.transform.position = this.gameObject.transform.position;
        }

        if (Return)
        {
           // Debug.Log("Returning");
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, Startpos, 10f * Time.deltaTime);
            if (this.gameObject.transform.position == Startpos)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition;
                Return = false;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Connect = true;
            player = other.gameObject;
            Prb = player.GetComponent<Rigidbody>();
            StartCoroutine(Launcher());

        }

        


    }

    private IEnumerator Launcher()
    {
        yield return new WaitForSeconds(1f);
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        springJoint.maxDistance = 1;
       // Connect = false;
        StartCoroutine(Launch());
    }

    private IEnumerator Launch()
    {
        yield return new WaitForSeconds(0.2f);
        Prb.velocity = rb.velocity * 2;
        Connect = false;
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        
        yield return new WaitForSeconds(2f);
        springJoint.maxDistance = ResetValue;
        Return = true;
       
    }
}
