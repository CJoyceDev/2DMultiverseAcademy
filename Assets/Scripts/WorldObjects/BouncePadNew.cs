using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadNew : MonoBehaviour
{
    [SerializeField] float bounce = 20f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bounce, ForceMode.Impulse);
            animator.Play("Base Layer.Bounce");
        }
    }
}

