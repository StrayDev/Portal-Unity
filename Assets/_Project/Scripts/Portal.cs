
using System;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //[SerializeField] private Camera cam = default;
    [SerializeField] private PortalCamera cameraScript = default;
    //[SerializeField] private MeshRenderer screen = default;

    private Transform player = default;
    private Vector3 lastOffset = default;

    private Transform thisPortal = default;
    private Transform otherPortal = default;

    private bool isActive = false;
    public bool IsActive
    {     
        set 
        { 
            isActive = value;
            cameraScript.enabled = value;
            SetOtherPortal(value);
        }
    }

    public Colour colour = Colour.None;
    public static List<Portal> Portals = new List<Portal>();

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        thisPortal = transform;
        lastOffset = player.position - transform.position;

    }

    private void LateUpdate()
    {
        if (!isActive) return;
        
        var dist = Vector3.Distance(player.position, transform.position);
        if (dist > 1) return;
        
        var offset = Camera.main.transform.position - transform.position;
        var portalSide = Math.Sign(Vector3.Dot(offset, transform.forward));
        var lastPortalSide = Math.Sign(Vector3.Dot(lastOffset, transform.forward));

        if (portalSide != lastPortalSide)
        {
            Debug.Log("YARRRRS");
            var m = otherPortal.localToWorldMatrix * thisPortal.worldToLocalMatrix * player.localToWorldMatrix;
            player.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
            player.transform.RotateAround(otherPortal.position, otherPortal.up, 180f);
            offset = Camera.main.transform.position - transform.position;
        }
        
        lastOffset = offset; //at end
    }

    private void OnEnable()
    {
        Portals.Add(this);
    }
    
    private void OnDisable()
    {
        Portals.Remove(this);
    }
    
    
    private void SetOtherPortal(bool value)
    {
        if (!value) return;
        
        foreach (var portal in Portals)
        {
            if (portal != this) otherPortal = portal.transform;
        }
    }
}

