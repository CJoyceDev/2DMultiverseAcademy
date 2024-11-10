using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperScript : MonoBehaviour
{

    [SerializeField] public GameObject FallingEnemy;
    [SerializeField] public GameObject FallingInstance;
    Vector3 Spawn;


    // Start is called before the first frame update
    void Start()
    {
          Spawn = new Vector3(transform.position.x, transform.position.y, transform.position.z);
       
        Drop();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Drop()
    {


        FallingInstance = Instantiate(FallingEnemy, Spawn, Quaternion.identity);

        StartCoroutine(DropWait());


    } 
    
    
    IEnumerator DropWait()
        {

        yield return new WaitForSeconds(3.0f);
            Destroy(FallingInstance);
            Drop();

        }
}
