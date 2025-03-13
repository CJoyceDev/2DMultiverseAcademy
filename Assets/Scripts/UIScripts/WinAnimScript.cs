using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimScript : MonoBehaviour
{
    public GameObject AnimObject;




    Animator animator;


    void Start()
    {





    }

    private void OnEnable()
    {
        animator = AnimObject.GetComponent<Animator>();
        animator.Play("Base Layer.WinAnim");
    }
}
