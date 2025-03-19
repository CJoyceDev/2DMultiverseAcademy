using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public bool isActivated = false;
    [SerializeField] float originalSpeed = 30;
    float moveScale = 0.005f;
    Vector3 newPos;
    float speed;
    [SerializeField] bool shakeUp;
    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
        speed = originalSpeed;
        speed += Random.Range(1, -1);
    }

    // Update is called once per frame
    void Update()
    {
      if (isActivated == true)
        {
            Shake();
        }
    }



    void Shake()
    {
        //random is used to make the two halfs shake independently CJ
         
       // speed += Random.Range(5, -5);


        newPos = transform.position;
        //newPos.x += Mathf.Sin(Time.time * speed)  * moveScale; 
        if (shakeUp)
        {
            newPos.z += Mathf.Sin(Time.time * speed) * moveScale;
        }
        else
        {
            newPos.z -= Mathf.Sin(Time.time * speed) * moveScale;
        }


            transform.position = newPos;
    }
}


