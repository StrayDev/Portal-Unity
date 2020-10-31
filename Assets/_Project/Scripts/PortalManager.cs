using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Colour { None, Blue, Orange }

public class PortalManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = default;
    [SerializeField] private GameObject portalPrefab = default;
    
    private void CreatePortal(Colour colour)
    {
        //check for valid Raycast
        var ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        if (!Physics.Raycast(ray, out var hit)) return;

        //if (Portal.PortalInProximity(hit.point)) return;
        
        //if there is already a portal of the same colour delete it
        if (Portal.Portals.Find(p => p.colour == colour))
        {
            Destroy(Portal.Portals.Find(p => p.colour == colour).gameObject);
            foreach (var p in Portal.Portals) p.IsActive = false;
        }
        
        //create a new portal and give it the correct colour
        var portal = Instantiate(portalPrefab).GetComponentInChildren<Portal>();
        portal.colour = colour;
        
        //set position and rotation
        var surfaceRotation = Quaternion.LookRotation(hit.normal);
        portal.transform.SetPositionAndRotation(hit.point, surfaceRotation);
        
        //if 2 portals set both to active
        if (Portal.Portals.Count > 1)
        {
            foreach (var p in Portal.Portals) p.IsActive = true;
        }
    }

    public void DestroyPortals()
    {
        //Clears all existing portals
        while (Portal.Portals.Count > 0)
        {
            Destroy(Portal.Portals[0].gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) CreatePortal(Colour.Blue);
        if(Input.GetMouseButtonDown(1)) CreatePortal(Colour.Orange);
        if(Input.GetMouseButtonDown(2)) DestroyPortals();
    }

    private GameObject GetOverlappingPortal(Vector3 position, Colour colour)
    {
        /////////////////////////////
        return null;
    }
}
