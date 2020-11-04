using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private BoxCollider hole = default;
    [SerializeField] private BoxCollider surface = default;

    [SerializeField] private Transform origin = default;

    private void Start()
    {
        Hole.CutHole(hole.bounds, surface.bounds, transform);
        surface.enabled = false;
        hole.enabled = false;
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawRay(origin.position + origin.forward * 0.1f, origin.right * 0.5f);
        Gizmos.DrawRay(origin.position + origin.forward * 0.1f, -origin.right * 0.5f);
        Gizmos.DrawRay(origin.position + origin.forward * 0.1f, origin.up * 1);
        Gizmos.DrawRay(origin.position + origin.forward * 0.1f, -origin.up * 1);
    }
}
