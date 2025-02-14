using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    //RS
    public GameObject CreatedObject;
    public GameObject CreatePoint;

    bool CanActivate = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateAbility()
    {
        //checks whether or not the ability is on cooldown
        if (CanActivate)
        {
            Debug.Log("Creation");
            Instantiate(CreatedObject, CreatePoint.transform.position, Quaternion.identity);//places the object at the CreatePoint location
            CanActivate = false;
            StartCoroutine(CoolDown());
        }
        else
        {
            Debug.Log("Wait For Cooldown");
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(5f);
        CanActivate = true;
    }
}
