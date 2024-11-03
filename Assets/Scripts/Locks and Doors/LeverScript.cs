using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    [SerializeField] GameObject Door;
    [SerializeField] GameObject Player;

    private Vector3 vel;
    private Vector3 Offset;
    public bool Pulled;
    public bool OpenUp;
    public float smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        Pulled = false;
        OpenUp = false;
        Offset = new Vector3(0, 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pulled)
        {
            if (!OpenUp)
            {
                
                Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position + Offset, ref vel, smoothTime);
                StartCoroutine(SmoothOpen());
            }
            

            
           
        }

        if (!Pulled)
        {
            if (OpenUp)
            {

                Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position - Offset, ref vel, smoothTime);
                StartCoroutine(SmoothClose());
            }



            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Pulled)
        {
           // if (Input.GetKeyDown(KeyCode.P))
          //  {
                Pulled = true;
         //   }
          
        }
          /*
        if (other.CompareTag("Player") && Pulled)
        {
             if (Input.GetKeyDown(KeyCode.P))
              {
            Pulled = false;
               }

       } */

    }

    IEnumerator SmoothOpen()
    {

        yield return new WaitForSeconds(0.5f);
        OpenUp = true;

    }
   
    IEnumerator SmoothClose()
    {

        yield return new WaitForSeconds(0.5f);
        OpenUp = false;

    }
}
