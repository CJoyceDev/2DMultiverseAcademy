using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] GameObject ShotPrefab;
    [SerializeField] Transform shootTransform;
    PlayerShot Shot;
    CJMovementWithRB pc;
    Rigidbody playerRB;


    private void Start()
    {
        pc = GetComponent<CJMovementWithRB>();
        playerRB = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

        if (InputHandler.Ability2Pressed && !pc.swapCD)
        {  
            /*Debug.Log("Pin");*/
            Shot = Instantiate(ShotPrefab, shootTransform.position, Quaternion.identity).GetComponent<PlayerShot>();
            Shot.Initialize(shootTransform,playerRB);
        }
        


    }

}
