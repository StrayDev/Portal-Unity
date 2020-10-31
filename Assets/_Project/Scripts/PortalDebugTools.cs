using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDebugTools : MonoBehaviour
{
    [SerializeField] private Camera camA = default;
    [SerializeField] private Transform portalA = default;
    
    [SerializeField] private Camera camB = default;
    [SerializeField] private Transform portalB = default;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(camA.transform.position, portalA.position);
        Gizmos.DrawLine(camB.transform.position, portalB.position);
        Gizmos.DrawLine(camB.transform.position, camA.transform.position);
    }
}
