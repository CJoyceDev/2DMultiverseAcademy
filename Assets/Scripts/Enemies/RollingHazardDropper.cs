using System.Collections;
using UnityEngine;

public class RollingHazardDropper : MonoBehaviour
{
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject FallingEnemy1;
    [SerializeField] private GameObject FallingEnemy2;
    [SerializeField] private GameObject FallingEnemy3;
    [SerializeField] private GameObject FallingEnemy4;
    [SerializeField] private float FallTime;
    [SerializeField] private float SpawnDelay = 2f;
    GameObject randomFallingEnemy;

    

  


    void Start()
    {
        StartCoroutine(SpawnHazards());
    }

    IEnumerator SpawnHazards()
    {
        while (true) 
        {
            int randomIndex = Random.Range(0, 4); 

            if (randomIndex == 0)
            {
                randomFallingEnemy = FallingEnemy1;
            }
            else if (randomIndex == 1)
            {
                randomFallingEnemy = FallingEnemy2;
            }
            else if (randomIndex == 2)
            {
                randomFallingEnemy = FallingEnemy3;
            }
            else
            {
                randomFallingEnemy = FallingEnemy4;
            }

            GameObject newHazard = Instantiate(randomFallingEnemy, spawnpoint.transform.position, Quaternion.Euler(0f, 0f, 0f));
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

