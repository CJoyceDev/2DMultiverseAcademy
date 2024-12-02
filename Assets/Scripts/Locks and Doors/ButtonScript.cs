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
    bool Boxpush;
    bool Playerpush;
    bool Hurtpush;
    bool moveFin;

    // Start is called before the first frame update
    void Start()
    {
        Pulled = false;
        OpenUp = false;
        Boxpush = false;
        Playerpush = false;
        Hurtpush = false;
        moveFin = false;

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


        if (other.CompareTag("Box") && !Pulled && !Playerpush && !Hurtpush)
        {
           
            Pulled = true;
            Boxpush = true;

        }


        if (other.CompareTag("Player") && !Pulled && !Boxpush && !Hurtpush)
        {

            Pulled = true;
            Playerpush = true;

        }


        if (other.CompareTag("HurtBox") && !Pulled && !Playerpush && !Boxpush)
        {

            Pulled = true;
            Hurtpush = true;

        }


    }

    void OnTriggerExit(Collider other)
    {

        // closes door when box is removed CD

        if (other.CompareTag("Box") && Pulled && Boxpush)
        {

            Pulled = false;
            Boxpush = false;

        }


        if (other.CompareTag("Player") && Pulled && Playerpush)
        {

            Pulled = false;
            Playerpush = false;

        }


        if (other.CompareTag("HurtBox") && Pulled && Hurtpush)
        {

            Pulled = false;
            Hurtpush = false;

        }
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
