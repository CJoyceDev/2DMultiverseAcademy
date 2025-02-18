using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{


    [SerializeField] GameObject ConnectedObject;
    ActivateScript activateScript;

    // Start is called before the first frame update
    void Start()
    {
        activateScript = ConnectedObject.GetComponent<ActivateScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")){

            activateScript.Activate();
            Debug.Log("Hit");

        }


    }


}
