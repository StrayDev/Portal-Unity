using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = default;
    [SerializeField] private GameObject portal1 = default;
    [SerializeField] private GameObject portal2 = default;

    //[SerializeField] private Transform player = default;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreatePortal(portal1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CreatePortal(portal2);
        }

        if (Input.GetMouseButtonDown(2))
        {
            DestroyAllPlayerPortals();
        }
    }

    private void DestroyAllPlayerPortals()
    {
        /*portal1.IsActive = false;
        portal2.IsActive = false;*/
    }

    private void CreatePortal(GameObject portal)
    {
        //portal.IsActive = true;
        
        var ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        if (!Physics.Raycast(ray, out var hit)) return;

        //if (Portal.PortalInProximity(hit.point)) return;

        var surfaceRotation = Quaternion.LookRotation(hit.normal);
        
        portal.transform.rotation = surfaceRotation;
        portal.transform.position = hit.point;
        
        
    }
}

