using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGrav : MonoBehaviour
{
    
    private ConstantForce Dropforce;
    Vector3 IncreaseGrav;
    Vector3 Grav;
    // code to make the drop fall slower than natural gravity, to give the player time to react CD

    // Start is called before the first frame update
    void Start()
    {
        // gets the constant force component that adds a downwards force to the object CD
        Dropforce = GetComponent<ConstantForce>();
        // set up that control the gravity the drop has, and a constant value to increase the gravity by CD
        IncreaseGrav = new Vector3(0.0f, -0.1f, 0.0f);
        Grav = new Vector3(0.0f, -0.2f, 0.0f);


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // simple additions to add the constant gravity to the garvity on the obeject CD
        Dropforce.force = Grav;
        Grav = Grav + IncreaseGrav;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
        
    }
}
