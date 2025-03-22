using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnAnimation : MonoBehaviour
{

    [SerializeField]GameObject player;
    [SerializeField]GameObject inputSystem;
    [SerializeField] ParticleSystem spawnPS;
    [SerializeField] AnimationHandler animSystem;

    [SerializeField] float playerHeight = 3f;
    [SerializeField] float animDuration = 1f; 
    
    void Start()
    {
        if (inputSystem != null)
        {
            inputSystem.SetActive(false);
            StartCoroutine(SpawnSequence());
            //animSystem.enabled = false;
        }
    }

    // Update is called once per frame
    IEnumerator SpawnSequence()
    {
        // Disable player movement
        //inputSystem.enabled = false;
        

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * playerHeight;
        float elapsedTime = 0f;

        while (elapsedTime < animDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
           
            yield return null;

        }
        
        transform.position = targetPos;

        inputSystem.SetActive(true);
        spawnPS.Stop();

        


        
       
}
    }

