using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    Vector3 SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("KillBox"))
        {
            Spawn();
        }

  


    }

    void Spawn()
    {

        transform.position = SpawnPoint;

    }
}
