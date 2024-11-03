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
        Offset = new Vector3(0, 0, 2);

    }

    // Update is called once per frame
    void Update()
    {
   
        if (OpenUp)
        {
            Debug.Log(OpenUp);

            transform.position = Vector3.SmoothDamp(transform.position, transform.position + Offset, ref vel, smoothTime);
            StartCoroutine(SmoothOpen());
        }

    }





   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key") && !OpenUp)
        {

            StartCoroutine(SmoothKey());
 
        }

    }

    IEnumerator SmoothKey()
    {
        yield return new WaitForSeconds(0.5f);
          Destroy(Key);
            OpenUp = true;
       
    }

    IEnumerator SmoothOpen()
    {
       
        yield return new WaitForSeconds(0.5f);
        OpenUp = false;

    }

}