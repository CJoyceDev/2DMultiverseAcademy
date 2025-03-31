using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light myLight;

    void Start() => myLight = GetComponent<Light>();

    void Update()
    {
        myLight.intensity = 2.5f + Mathf.PerlinNoise(Time.time * 4f, 0f) * 1.2f;
    }
}
