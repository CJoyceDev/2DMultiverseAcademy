using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] public GameObject Enemy;
    [SerializeField] public GameObject Instance;


    Vector3 ESpawn;


    // Start is called before the first frame update
    void Start()
    {
        ESpawn = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Spawn();

    }


    void Spawn()
    {

        // creates the falling drop at the center of the object CD
        Instance = Instantiate(Enemy, ESpawn, Quaternion.identity);

        StartCoroutine(SpawnMore());


    }


    IEnumerator SpawnMore()
    {
        // gives time for the drop to fall before destroying it and making a new one CD
        yield return new WaitForSeconds(10f);
       
        Spawn();

    }
}
