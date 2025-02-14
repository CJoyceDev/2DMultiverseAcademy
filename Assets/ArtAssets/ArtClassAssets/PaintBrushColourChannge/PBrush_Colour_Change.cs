using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBrush_Colour_Change : MonoBehaviour
{
    public Material paintbrushMaterial;

    public Color myColor;
    void Start()
    {
        
    }


    void Update()
    {
        paintbrushMaterial.color = myColor;
    }
}
