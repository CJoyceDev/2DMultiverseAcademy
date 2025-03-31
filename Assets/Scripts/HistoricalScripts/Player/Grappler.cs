using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] float pullSpeed = 2f;
    [SerializeField] float pullSpeedY = 3f;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;
    CJMovementWithRB pc;
    Hook hook;
    bool pulling;
    bool Active;
    bool ReturnHook;
    Rigidbody rb;
    Rigidbody Prb;
    private List<GameObject> pullObjects;
    public Vector3 pullDirection;
    [SerializeField] float RopeLength;
    public bool delaytime = true;
   
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pulling = false;
        pullObjects = new List<GameObject>();
        pc = GetComponent<CJMovementWithRB>();
        Active = true;


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Active)
        {
            if (delaytime) //If the time elapsed is more than the fire rate, allow a shot
                 {
            // spawns and despawns the hook on button press
                     if (hook == null && (InputHandler.Ability1Pressed || InputHandler.Ability1Held) && !ReturnHook)
                         {
 
                         StopAllCoroutines();
                         pulling = false;
                          hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
                           hook.Initialize(this, shootTransform);
                    //StartCoroutine(RetriveHook());
                            delaytime = false;
                            StartCoroutine(Cooldown());
                         }
                     else if (hook != null && (InputHandler.Ability1Pressed || InputHandler.Ability1Held) && !ReturnHook)
                     {
                         delaytime = false;
                         StartCoroutine(Cooldown()); 
                          ReturnHook = true;
                      }
                 //set new time of last shot
            }


            if (hook != null && ReturnHook)
            {
                RetriveHook();
            }
            else
            {
                ReturnHook = false;
            }

            // if the hook is pulling
            if (pulling)
            {
                foreach (GameObject obj in pullObjects)
                {
                    Prb = obj.GetComponent<Rigidbody>();

                    if (!pulling || hook == null) { return; }


                    // for pulling the box along x CD
                    if ((rb.transform.position.x  - Prb.transform.position.x) > 0)
                    {
                        pullDirection = new Vector3(rb.transform.position.x - Prb.transform.position.x - RopeLength, 0, 0);

                        //Make it so evie can only pull not push CD
                        if ((Time.deltaTime * pullSpeed * pullDirection).x > 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                            hook.transform.position = Prb.transform.position;
                        }


                    }
                    else if ((pc.rb.transform.position.x - Prb.transform.position.x) < 0)
                    {
                        pullDirection = new Vector3(pc.rb.transform.position.x - Prb.transform.position.x + RopeLength, 0, 0);
                        if ((Time.deltaTime * pullSpeed * pullDirection).x < 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                            hook.transform.position = Prb.transform.position;
                        }

                    }

                    // for pulling the box along y 
                    if ((rb.transform.position.y - Prb.transform.position.y) > 0)
                    {
                        pullDirection = new Vector3(0, rb.transform.position.y - Prb.transform.position.y - RopeLength, 0);

                        //Make it so evie can only pull not push CD
                        if ((Time.deltaTime * pullSpeedY * pullDirection).y > 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeedY * pullDirection);
                            hook.transform.position = Prb.transform.position;
                        }


                    }
                    else if ((pc.rb.transform.position.y - Prb.transform.position.y) < 0)
                    {
                        pullDirection = new Vector3(0, pc.rb.transform.position.y - Prb.transform.position.y + RopeLength, 0);
                        if ((Time.deltaTime * pullSpeedY * pullDirection).x < 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeedY * pullDirection);
                            hook.transform.position = Prb.transform.position;
                        }

                    }


                }
            }


        }
    }
    //Get the boxes collider and start pulling after the hook hitsCD
    public void StartPull(Collider other)
    {
        if (!pulling) { 
        pullObjects.Clear();
        pullObjects.Add(other.gameObject);
        Debug.Log("pull");
        pulling = true;
         }
    }
    //destroys the hook and stops pulling CD
    private void DestroyHook()
    {
        if (hook == null) return;
        pullObjects.Clear();
        pulling = false;
        Destroy(hook.gameObject);
        hook = null;
    }
    //timer for how long the hook lasts , might remove CD
    private IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(4f);

        DestroyHook();
    }

    private void RetriveHook()
    {
       // Debug.Log("Retrive");
        if(hook != null) { 

        
        hook.transform.position = Vector3.MoveTowards(hook.transform.position, shootTransform.position, 100f * Time.deltaTime);

       // (Vector3.MoveTowards(hook.transform.position, shootTransform.position, 60f * Time.deltaTime));

        if (hook.transform.position == shootTransform.position)
        {
            ReturnHook = false;
            DestroyHook();
        }

        }
        pullObjects.Clear();

    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.1f);

        delaytime = true;
    }

    // For The player to only use ability as evie CD
    public void ActivateAbility()
    {
        Active = true;
    }

    public void DeActivateAbility()
    {
        Active = false;
    }
}

