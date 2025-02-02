using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJMovementScriptNoRB : MonoBehaviour
{

    [SerializeField] int Speed = 3;
    [SerializeField] int gravityForce = -1;
    Vector3 move;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"),gravityForce,0);
        transform.Translate(move * Speed * Time.deltaTime);
    }
}
