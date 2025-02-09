using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsCriptRight : MonoBehaviour
{
    public Vector3 ghostPosRight;
    // Start is called before the first frame update

    //used by moving platform script to place ghost platforms CJ
    void Start()
    {
        ghostPosRight = transform.position;
    }


}
