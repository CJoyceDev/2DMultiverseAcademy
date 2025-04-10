using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnAnimation : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField]GameObject inputSystem;
    [SerializeField] GameObject spawnPS;
    [SerializeField] AnimationHandler animSystem;
    [SerializeField] GameObject portalImage;
    GameObject effectsInstance;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float animDuration = 0.2f;
    [SerializeField] AudioClip PostitSound;

    public static bool isSpawning = true;
    
    void Start()
    {
        isSpawning = true;

        SoundHandler.instance.PlaySound(PostitSound, transform, 0.25f, 1.5f);

        if (inputSystem != null)
        {
            inputSystem.SetActive(false);
            StartCoroutine(SpawnSequence());
            //GameObject effectsInstance = Instantiate(spawnPS, transform.position + new Vector3(0, -playerHeight / 2, 0), Quaternion.Euler(90, 0, 0));
            GameObject effectsInstance = Instantiate(spawnPS, transform.position + new Vector3(0, 0.4f, -1.3f), Quaternion.Euler(0, 0, 0));
            Destroy(effectsInstance, 1f);

            animSystem.enabled = false;
            //portalImage.SetActive(true);
        }
    }

  
    

    // Update is called once per frame
    IEnumerator SpawnSequence()
    {
        

        Vector3 startPos = player.transform.localPosition + new Vector3(0, -playerHeight, 0);
        Vector3 targetPos = Vector3.zero;

        player.transform.localPosition = player.transform.localPosition + new Vector3(0,-playerHeight, 0);

        float elapsedTime = 0f;

        

        while (elapsedTime < animDuration - 0.15f)
        {
            player.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / animDuration);
            elapsedTime += Time.deltaTime;
           
            yield return null;

        }
        
        player.transform.localPosition = targetPos;

        inputSystem.SetActive(true);
        isSpawning = false;
 
        //portalImage.SetActive(false);
        animSystem.enabled = true;
        

    }
    }

