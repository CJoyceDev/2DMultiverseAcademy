using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{

    [SerializeField] GameObject Key;
    
    private Vector3 vel;
    private Vector3 Offset;
    public bool OpenUp;
    public float smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        OpenUp = false;
        Offset = new Vector3(0, 0, 2.5f);

    }

    // Update is called once per frame
    void Update()
    {
           // Checks to see if the key has been recived to open the door CD
        if (OpenUp)
        {
            Debug.Log(OpenUp);

            transform.position = Vector3.SmoothDamp(transform.position, transform.position + Offset, ref vel, smoothTime);
            StartCoroutine(SmoothOpen());
        }

    }




    // checks to see if the key is present CD
   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key") && !OpenUp)
        {

            StartCoroutine(SmoothKey());
 
        }

    }

    // stops the code to only destroy the key after its hidden inside the door CD
    IEnumerator SmoothKey()
    {
        yield return new WaitForSeconds(0.5f);
          Destroy(Key);
            OpenUp = true;
       
    }

    //Controls how long the door opens for CD
    IEnumerator SmoothOpen()
    {
       
        yield return new WaitForSeconds(0.5f);
        OpenUp = false;

    }

}