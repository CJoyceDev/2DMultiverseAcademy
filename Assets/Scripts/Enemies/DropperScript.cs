using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperScript : MonoBehaviour
{

    [SerializeField] public GameObject FallingEnemy;
    [SerializeField] public GameObject FallingInstance;
    [SerializeField] public float FallTime;
    
    Vector3 Spawn;


    // Start is called before the first frame update
    void Start()
    {
        Spawn = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Drop();


    }


    void Drop()
    {

    // creates the falling drop at the center of the object CD
        FallingInstance = Instantiate(FallingEnemy, Spawn, Quaternion.identity);

        StartCoroutine(DropWait());


    } 
    
    
    IEnumerator DropWait()
        {
            // gives time for the drop to fall before destroying it and making a new one CD
        yield return new WaitForSeconds(FallTime);
            Destroy(FallingInstance);
            Drop();

        }
}
