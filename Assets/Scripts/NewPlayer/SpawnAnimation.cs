using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnAnimation : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField]GameObject inputSystem;
    [SerializeField] GameObject spawnPS;
    [SerializeField] AnimationHandler animSystem;

    [SerializeField] float playerHeight = 2f;
    [SerializeField] float animDuration = 1f;

    public static bool isSpawning = true;
    
    void Start()
    {
        isSpawning = true;


        if (inputSystem != null)
        {
            inputSystem.SetActive(false);
            StartCoroutine(SpawnSequence());
            GameObject EffectsInstance = Instantiate(spawnPS, transform.position + new Vector3(0, -playerHeight/2, 0), Quaternion.identity);
            Destroy(EffectsInstance, 1.0f);
            //animSystem.enabled = false;
        }
    }

  
    

    // Update is called once per frame
    IEnumerator SpawnSequence()
    {
       

        Vector3 startPos = player.transform.localPosition + new Vector3(0, -playerHeight, 0);
        Vector3 targetPos = Vector3.zero;

        player.transform.localPosition = player.transform.localPosition + new Vector3(0,-playerHeight, 0);

        float elapsedTime = 0f;

        

        while (elapsedTime < animDuration)
        {
            player.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
           
            yield return null;

        }
        
        player.transform.localPosition = targetPos;

        inputSystem.SetActive(true);

        isSpawning = false;

}
    }

