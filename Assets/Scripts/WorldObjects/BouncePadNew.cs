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
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            CJMovementWithRB MS = collision.gameObject.GetComponent<CJMovementWithRB>();

            MS.PlaySound(MS.BounceSound);

            if (rb != null)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
                rb.AddForce(transform.up * bounce, ForceMode.VelocityChange);
            }
        }
    }

}

