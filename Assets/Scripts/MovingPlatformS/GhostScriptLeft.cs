using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostScriptLeft : MonoBehaviour
{
    public Vector3 ghostPosLeft;
    // Start is called before the first frame update

    //used by moving platform script to place ghost platforms CJ
    void Start()
    {
        ghostPosLeft = transform.position;
        
    }



}