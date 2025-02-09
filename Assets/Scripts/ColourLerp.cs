using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourLerp : MonoBehaviour
{
    public Color First = Color.magenta;
    public Color Second = Color.blue;
    public float speed = 1.0f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.color = Color.Lerp(First, Second, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
