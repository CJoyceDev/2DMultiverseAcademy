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
        // creates the Enemy at the center of the object CD
        Instance = Instantiate(Enemy, ESpawn, Quaternion.identity);
        StartCoroutine(SpawnMore());
    }


    IEnumerator SpawnMore()
    {
        // Delay between each spawn
        yield return new WaitForSeconds(10f);
        Spawn();

    }
}
