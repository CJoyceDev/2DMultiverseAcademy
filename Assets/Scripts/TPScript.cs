using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPScript : MonoBehaviour
{

    GameObject LastObjectHit;

    [SerializeField] GameObject OtherEnd;
    public Rigidbody rb;
    [SerializeField] Vector3 LeaveForce;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            LastObjectHit = other.gameObject;
            rb = LastObjectHit.GetComponent<Rigidbody>();
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = OtherEnd.transform.position;
            StartCoroutine(Wait());


        }


    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        LastObjectHit.SetActive(true);
        rb.AddForce(LeaveForce);

    }

}
