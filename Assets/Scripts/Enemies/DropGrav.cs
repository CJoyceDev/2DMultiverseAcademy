using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGrav : MonoBehaviour
{
    private ConstantForce Dropforce;
    [SerializeField] Vector3 IncreaseGrav;
    [SerializeField] Vector3 Grav;

    // Start is called before the first frame update
    void Start()
    {
        Dropforce = GetComponent<ConstantForce>();
        IncreaseGrav = new Vector3(0.0f, -0.1f, 0.0f);
        Grav = new Vector3(0.0f, -0.2f, 0.0f);


    }

    // Update is called once per frame
    void FixedUpdate()
    {


        Dropforce.force = Grav;
        Grav = Grav + IncreaseGrav;


    }
}
