using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    Vector3 _velocity, _acceleration;

    float _maxVelocity;

    bool _gravityOn;

    private void FixedUpdate()
    {
        transform.position += _velocity * _maxVelocity * Time.deltaTime;
    }

}
