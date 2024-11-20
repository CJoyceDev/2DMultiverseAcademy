using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsScript : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    //simple script to make coin assets rotate CJ
    void Update()
    {
        float angle = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * angle, Space.World);
    }


    /* // Commented out to not contradict with other coin code CD
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }



    }
    */
}
