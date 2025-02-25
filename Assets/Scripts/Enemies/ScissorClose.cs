using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorClose : MonoBehaviour
{

    [SerializeField] Transform CenterPoint;
    [SerializeField] GameObject ObjectPoint;    
    [SerializeField] float RotateSpeed;
    bool Cut = false;
    bool Open = false;
    // Start is called before the first frame update
    void Start()
    {

        //Rotate(Vector3.up * RotateSpeed * Time.deltaTime);

        //StartCoroutine(CloseOpen());
    }

    // Update is called once per frame
    void FixedUpdate()
    {


         if (ObjectPoint.transform.rotation.z  < 0)
        {
            Open = true;

        }
        if (ObjectPoint.transform.rotation.z < 90 && ObjectPoint.transform.rotation.z > 0) {
           Open = false;

        }
      




        if (Open)
        {
            ObjectPoint.transform.RotateAround(CenterPoint.position ,Vector3.forward , RotateSpeed * Time.deltaTime);
            Cut = false;
           
        }
        if (!Open)
        {
            ObjectPoint.transform.RotateAround(CenterPoint.position, Vector3.back, RotateSpeed * Time.deltaTime);
            Cut = true;
           
        }





    }

    private IEnumerator CloseOpen()
    {

        while (Cut) {
           
        yield return new WaitForSeconds(3f);
            
         }

    


    }


    private IEnumerator OpenClose()
    {
   
        while (!Cut)
        {
           
            yield return new WaitForSeconds(3f);

        }


    }

}
