using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHazard : MonoBehaviour
{

    [SerializeField] SphereCollider bc;
    [SerializeField] float timeToClose;

    bool bookOpen = true;

    [SerializeField] Animator animator;

    private AudioSource audioPlayer;

    

    private void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    //Start timer to end you //PD
    public void StartBookHazard()
    {
        if (bookOpen)
        {
            /*Debug.Log("A");*/
            StartCoroutine(CloseBook(timeToClose));
        }
        
    }
    //After timer show trigger colider that kills the player //PD
    IEnumerator CloseBook(float time)
    {
        bookOpen = false;
        //Play Animation Here //PD
        animator.Play("Base Layer.BookClose");
        audioPlayer.time = 0.1f;
        audioPlayer.Play();
        float timeTotal = 0;

        while(timeTotal < time)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
        
        /*Debug.Log("B");*/

        bc.enabled = true;
        StartCoroutine(ResetBook());

    }
    //resets the book to be open and ready to flatten you once more //PD
    IEnumerator ResetBook()
    {
        float timeTotal = 0;
        animator.Play("Base Layer.BookOpen");

        while (timeTotal < .1f)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
       /* Debug.Log("C");*/
        bc.enabled = false;
        bookOpen = true;

    }


}
