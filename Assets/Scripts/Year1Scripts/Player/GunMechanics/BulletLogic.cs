using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame


    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

}