using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] float pullSpeed = 0.1f;
    //[SerializeField] float stopDistance = 2.0f;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;
    PlayerController pc;
    Hook hook;
    bool pulling;
    bool Active;
    Rigidbody rb;
    Rigidbody Prb;
    private List<GameObject> pullObjects;
    public Vector3 pullDirection;
    [SerializeField] float RopeLength;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pulling = false;
        pullObjects = new List<GameObject>();
        pc = GetComponent<PlayerController>();
        Active = false;


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Active)
        {
            // spawns and despawns the hook on button press
            if (hook == null && pc.inputActions.Player.Ability.ReadValue<float>() > 0)
            {

                StopAllCoroutines();
                pulling = false;
                hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
                hook.Initialize(this, shootTransform);
                StartCoroutine(DestroyHookAfterLifetime());

            }
            else if (hook != null && pc.inputActions.Player.Ability.ReadValue<float>() > 0)
            {
                DestroyHook();
            }




            // if the hook is pulling
            if (pulling)
            {
                foreach (GameObject obj in pullObjects)
                {
                    Prb = obj.GetComponent<Rigidbody>();

                    if (!pulling || hook == null) { return; }


                    // for pulling the box along x CD
                    if ((rb.transform.position.x - Prb.transform.position.x) > 0)
                    {
                        pullDirection = new Vector3(rb.transform.position.x - Prb.transform.position.x - RopeLength, 0, 0);

                        //Make it so evie can only pull not push CD
                        if ((Time.deltaTime * pullSpeed * pullDirection).x > 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                            hook.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                        }


                    }
                    else if ((pc.rb.transform.position.x - Prb.transform.position.x) < 0)
                    {
                        pullDirection = new Vector3(pc.rb.transform.position.x - Prb.transform.position.x + RopeLength, 0, 0);
                        if ((Time.deltaTime * pullSpeed * pullDirection).x < 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                            hook.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                        }

                    }

                    // for pulling the box along y ,, DOSENT APPEAR TO WORK? FIND OUT WHY CD
                    if ((rb.transform.position.y - Prb.transform.position.y) > 0)
                    {
                        pullDirection = new Vector3(0, rb.transform.position.y - Prb.transform.position.y - RopeLength, 0);

                        //Make it so evie can only pull not push CD
                        if ((Time.deltaTime * pullSpeed * pullDirection).y > 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                            hook.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                        }


                    }
                    else if ((pc.rb.transform.position.y - Prb.transform.position.y) < 0)
                    {
                        pullDirection = new Vector3(0, pc.rb.transform.position.y - Prb.transform.position.y + RopeLength, 0);
                        if ((Time.deltaTime * pullSpeed * pullDirection).x < 0)
                        {
                            Prb.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                            hook.transform.Translate(Time.deltaTime * pullSpeed * pullDirection);
                        }

                    }


                }
            }


        }
    }
    //Get the boxes collider and start pulling after the hook hitsCD
    public void StartPull(Collider other)
    {
        pullObjects.Add(other.gameObject);
        Debug.Log("pull");
        pulling = true;
    }
    //destroys the hook and stops pulling CD
    private void DestroyHook()
    {
        if (hook == null) return;

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

    public void ActivateAbility()
    {
        Active = true;



    }


    public void DeActivateAbility()
    {
        Active = false;


    }
}
