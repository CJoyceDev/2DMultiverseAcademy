using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorClose : MonoBehaviour
{

    [SerializeField] Transform CenterPoint;
    [SerializeField] GameObject ObjectPoint;    
    [SerializeField] float RotateSpeed;
    [SerializeField] int MaxCuts;
    int Cuts = 0;
    bool CutStop = false;
    public bool Open = false;
   
    // Start is called before the first frame update
    void Start()
    {

        //Rotate(Vector3.up * RotateSpeed * Time.deltaTime);

        //StartCoroutine(CloseOpen());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(ObjectPoint.transform.localRotation.x);
        //Debug.Log(Cuts);
        if (ObjectPoint.transform.localRotation.x < 0.90 && ObjectPoint.transform.localRotation.x > 0.85 && Open == true && CutStop == false) {
           // Debug.Log("Close");
            Open = false;

        }
        if (ObjectPoint.transform.localRotation.x  < 0 && ObjectPoint.transform.localRotation.x > -0.05 && Open == false && CutStop == false)
        {
           // Debug.Log("open");
            Open = true;
            Cuts = Cuts + 1;

        }

        if (Cuts == MaxCuts)
        {
            CutStop = true;
            StartCoroutine(Delay());

        }


        if (Open && CutStop == false)
        {
            ObjectPoint.transform.RotateAround(CenterPoint.position ,Vector3.back , RotateSpeed * Time.deltaTime);
            
           
        }
        if (!Open && CutStop == false)
        {
            ObjectPoint.transform.RotateAround(CenterPoint.position, Vector3.forward, RotateSpeed * Time.deltaTime);
           
           
        }





    }

 

    private IEnumerator Delay()
    {
   
           
        yield return new WaitForSeconds(2f);
        CutStop = false;
        Cuts = 0;
        StopCoroutine(Delay());
        
       


    }

}
