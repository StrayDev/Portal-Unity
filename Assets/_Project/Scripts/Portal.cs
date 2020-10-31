using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Camera cam = default;
    [SerializeField] private PortalCamera cameraScript = default;
    [SerializeField] private MeshRenderer screen = default;

    private bool isActive = false;
    public bool IsActive
    {     
        
        set 
        { 
            isActive = value;
            Debug.Log("ENABLED");
            cameraScript.enabled = value;
        }
    }

    public Colour colour = Colour.None;
    public static List<Portal> Portals = new List<Portal>();

    private void OnEnable()
    {
        Portals.Add(this);
    }

    public void SetTransform(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

    public void SetMaterial()
    {
        
    }
    
    private void OnDisable()
    {
        Portals.Remove(this);
    }
}
