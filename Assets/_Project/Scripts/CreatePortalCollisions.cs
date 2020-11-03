using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Events;

public class CreatePortalCollisions : MonoBehaviour
{
    [SerializeField] private BoxCollider portal = default;
    
    private Vector3 centerPosition = default;
    private Vector3 halfExtents = default;
    
    private List<BoxCollider> boxColliders = new List<BoxCollider>();
    private List<GameObject> gameObjects = new List<GameObject>();
    private bool hasRun = false;
    
    private void Start()
    {
        centerPosition = transform.position + transform.forward;
        halfExtents = Vector3.one * 2;

        foreach (var c in Physics.OverlapBox(centerPosition, halfExtents))
        {
            if (c.gameObject.tag == "Portal" || c.gameObject.tag == "Player") continue;
            
            var boxCollider = c.gameObject.GetComponent<BoxCollider>();
            var newObject = new GameObject("Copy : " + c.gameObject.name);
            var newBox = newObject.AddComponent<BoxCollider>();
            
            newBox.size = boxCollider.size;
            newBox.center = boxCollider.center;

            newObject.transform.parent = transform;
            newObject.transform.SetPositionAndRotation(c.transform.position, c.transform.rotation);
            newObject.layer = 8;
            
            boxColliders.Add(newBox);
            gameObjects.Add(newObject);
        }
    }

    public void Update()
    {
        if (hasRun) return; 
        
        foreach (var bc in boxColliders)
        {
            if (bc.bounds.Intersects(portal.bounds))
            {
                Debug.Log("FIRED ::");
                createNewCollisions(bc);
            }
        }

        hasRun = true;
    }

    private void createNewCollisions(BoxCollider box)
    {
        /*var bb = box.bounds;
        var bx = box.bounds.extents;
        var pb = portal.bounds;
        var px = portal.bounds.extents;
        box.size = Vector3.zero;

        //left
        var center = new Vector3();
        var width = Vector3.Distance(pb.center + new Vector3(-px.x, 0, 0), bb.center + new Vector3(bx, 0, 0));
        var leftTopBack = pb.center + new Vector3(-bx.x, bx.y, -bx.z);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.25f);
        Gizmos.DrawCube(centerPosition, halfExtents*2);
        
        //Gizmos.color = new Color(0,0,1,0.25f);
        //Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1f));
    }
}
