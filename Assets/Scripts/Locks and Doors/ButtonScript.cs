using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] GameObject Door;
    [SerializeField] GameObject Interacter;

    public bool moveup= false;
    public bool moveback = false;
    public bool movedown = false;
    public bool moveright = false;
    public bool moveleft = false;

    private Vector3 vel;
    private Vector3 ZOffset;
    private Vector3 XOffset;
    private Vector3 YOffset;
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
        moveFin = true;


    ZOffset = new Vector3(0, 0, 0.1f);
        XOffset = new Vector3(0.1f, 0, 0);
        YOffset = new Vector3(0, 0.1f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Pulled)
        {
            if (!OpenUp)
            {



                // Code to open the door if the button is pressed and it is closed CD

                if (moveback) {
                Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position + ZOffset, ref vel, smoothTime);
                StartCoroutine(SmoothOpen()); 
                 }
                
                if (moveup)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position + YOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothOpen());
                }
                if (movedown)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position - YOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothOpen());
                }
                if (moveright)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position + XOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothOpen());
                }
                if (moveleft)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position - XOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothOpen());
                }
               

            }




        }

        if (!Pulled)
        {
            if (OpenUp)
            {




                //Code to open the door if the button is pressed back and the door is open CD
                if (moveback) {
                Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position - ZOffset, ref vel, smoothTime);
                StartCoroutine(SmoothClose()); 
                }
                if (moveup)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position - YOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothClose());
                }
                if (movedown)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position + YOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothOpen());
                }
                if (moveright)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position - XOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothOpen());
                }
                if (moveleft)
                {
                    Door.transform.position = Vector3.SmoothDamp(Door.transform.position, Door.transform.position + XOffset, ref vel, smoothTime);
                    StartCoroutine(SmoothOpen());
                }

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
