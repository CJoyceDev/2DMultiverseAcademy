
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTracer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        // Update the trail during the jump
        positions.Add(transform.position);
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
