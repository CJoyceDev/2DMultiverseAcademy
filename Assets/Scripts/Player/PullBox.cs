using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBox : MonoBehaviour
{
    PlayerController pc;
    public bool abilityEnabled = false;
    Rigidbody rb;
    bool TouchBox;
    public Vector3 pullDirection;
    public float pullSpeed;
    private List<GameObject> pullObjects;


    // Start is called before the first frame update
    void Start()
    {
        // makes the empty list CD
        pullObjects = new List<GameObject>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //turn on and off grabbing funtionality CD
        Debug.Log(abilityEnabled);
        if (pc.inputActions.Player.Ability.ReadValue<float>() > 0 && !abilityEnabled)
        {

            abilityEnabled = true;
            Debug.Log("Press1");
        }
        else if (pc.inputActions.Player.Ability.ReadValue<float>() > 0 && abilityEnabled)
        {

            abilityEnabled = false;
            Debug.Log("Press2");
        }

        if (abilityEnabled)
        {

            foreach (GameObject obj in pullObjects)
            {
                // Check for which side of the cube Evie is on CD
                if((pc.rb.transform.position.x - obj.transform.position.x) > 0)
                {
                pullDirection = new Vector3(pc.rb.transform.position.x - obj.transform.position.x - 0.75f, 0, 0);

                    //Make it so evie can only pull not push CD
                    if((Time.deltaTime * pullSpeed * pullDirection).x > 0)
                    {
                        obj.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                    }
                

                }
                else if ((pc.rb.transform.position.x - obj.transform.position.x) < 0)
                {
                    pullDirection = new Vector3(pc.rb.transform.position.x - obj.transform.position.x + 0.75f, 0, 0);
                    if ((Time.deltaTime * pullSpeed * pullDirection).x < 0)
                    {
                        obj.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                    }

                }



            }
        }

        //makes evie let go if the button is pressed againCD
        else if (!abilityEnabled)
        {

            for (int i = 0; i < pullObjects.Count; i++)
            {
                pullObjects.RemoveAt(i);
            }
        }
       
    }

    void OnTriggerStay(Collider other)
    {
        // checks if evies next to a box while the ability is active to let her start pulling CD
        if (other.CompareTag("Box"))
        {
            
           // Debug.Log("touch");

         
                if (abilityEnabled)
                {
         
                pullObjects.Add(other.gameObject);
                TouchBox = true;
    
                 }
                else if (!abilityEnabled)
                 {
                TouchBox = false;
                pullObjects.Remove(other.gameObject);

                }


            }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Box"))
        {
            TouchBox = false;
            pullObjects.Remove(other.gameObject);
        }

    }




    public void ActivateAbility()
    {
       


    }
}
