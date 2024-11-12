using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] GameObject Door;
    [SerializeField] GameObject Interacter;

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
        Offset = new Vector3(0, 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pulled)
        {
            if (!OpenUp)
            {
                // Code to open the door if the lever is pulled and it is closed CD
                Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position + Offset, ref vel, smoothTime);
                StartCoroutine(SmoothOpen());
            }




        }

        if (!Pulled)
        {
            if (OpenUp)
            {
                //Code to open the door if the lever is pulled back and the door is open CD
                Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position - Offset, ref vel, smoothTime);
                StartCoroutine(SmoothClose());
            }




        }
    }

    void OnTriggerEnter(Collider other)
    {

        // when the box on button to press it CD


        if (other.CompareTag("Box") && !Pulled)
        {
           
            Pulled = true;
           

        }
      

    }

    void OnTriggerExit()
    {

        // closes door when box is removed CD

             Pulled = false;
    }


    // Code to make the door move smoothly for only a certain amount of time CD
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
