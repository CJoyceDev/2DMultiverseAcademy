using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
     public bool isActivated = false;
    float speed = 30;
    float amount = 0.005f;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
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
        newPos = transform.position;
        newPos.x += Mathf.Sin(Time.time * speed) * amount;

        transform.position = newPos;
    }
}


