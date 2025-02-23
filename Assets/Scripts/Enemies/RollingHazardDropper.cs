using System.Collections;
using UnityEngine;

public class RollingHazardDropper : MonoBehaviour
{
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject FallingEnemy;
    [SerializeField] private float FallTime;
    [SerializeField] private float SpawnDelay = 2f; 

    void Start()
    {
        StartCoroutine(SpawnHazards());
    }

    IEnumerator SpawnHazards()
    {
        while (true) 
        {
            GameObject newHazard = Instantiate(FallingEnemy, spawnpoint.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            StartCoroutine(DestroyAfterTime(newHazard, FallTime));
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    IEnumerator DestroyAfterTime(GameObject hazard, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(hazard);
    }
}

