using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    [SerializeField] GameObject Door;
    [SerializeField] GameObject LeverBrush;

    public bool moveup = false;
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

    // Start is called before the first frame update
    void Start()
    {
        Pulled = false;
        OpenUp = false;
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
                // Code to open the door if the lever is pulled and it is closed CD

                

                if (moveback)
                {
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
                //Code to open the door if the lever is pulled back and the door is open CD
                if (moveback)
                {
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

        // when the player passes infront of the lever to pull it CD
        // should make a button press? CD
        if (other.CompareTag("Player") && !Pulled)
        {
            // if (Input.GetKeyDown(KeyCode.P))
            //  {
            if (!Pulled)
            {
                LeverBrush.transform.Rotate(0.0f, 0.0f, -45.0f, Space.World);
            }

                Pulled = true;
                

         //   }
          
        }

       
      

    }

    // Code to make the door move smoothly for only a certain amount of time CD
    IEnumerator SmoothOpen()
    {
       
        yield return new WaitForSeconds(0.2f);
        OpenUp = true;

    }
   
    IEnumerator SmoothClose()
    {

        yield return new WaitForSeconds(0.2f);
        OpenUp = false;

    }
}
