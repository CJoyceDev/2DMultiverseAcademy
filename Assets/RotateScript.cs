using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(1, 1.5f, 0) * 20 * Time.deltaTime);
    }
}
