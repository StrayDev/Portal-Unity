using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private MeshCollider box = default;
    [SerializeField] private MeshCollider wall = default;
    
    private Vector3 center = new Vector3();
    private Vector3 size = new Vector3();

    void Start() 
    {
        if (box.bounds.Intersects(wall.bounds))
        {
           LeftMesh(box.bounds.center, box.bounds.extents, wall.bounds.center, wall.bounds.extents);
           RightMesh(box.bounds.center, box.bounds.extents, wall.bounds.center, wall.bounds.extents);


        }
    }

    private void LeftMesh(Vector3 bc, Vector3 bx, Vector3 wc, Vector3 wx)
    {
        if (bc.x - bx.x < wc.x - wx.x) return;
        
        center = wc;
        size = wall.bounds.size;
        center.x = (bc.x - bx.x + wc.x - wx.x) / 2;
        size.x = Vector3.Distance(bc + new Vector3(-bx.x, 0, 0), wc + new Vector3(-wx.x, 0, 0));
            
        var go = new GameObject();
        var col = go.AddComponent<BoxCollider>();

        col.center = center;
        col.size = size;

        go.transform.parent = transform;
    }
    
    private void RightMesh(Vector3 bc, Vector3 bx, Vector3 wc, Vector3 wx)
    {
        if (bc.x + bx.x > wc.x + wx.x) return;
        
        center = wc;
        size = wall.bounds.size;
        center.x = (bc.x + bx.x + wc.x + wx.x) / 2;
        size.x = Vector3.Distance(bc + new Vector3(bx.x, 0, 0), wc + new Vector3(wx.x, 0, 0));
            
        var go = new GameObject();
        var col = go.AddComponent<BoxCollider>();

        col.center = center;
        col.size = size;

        go.transform.parent = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(center, size);
    }
}
