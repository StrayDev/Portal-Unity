using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private BoxCollider box = default;
    [SerializeField] private BoxCollider wall = default;
    
    void Start() 
    {
        if (box.bounds.Intersects(wall.bounds))
        {
           CutHole(box.bounds, wall.bounds);
           wall.enabled = false;
           box.enabled = false;
        }
    }
    
    public void CutHole(Bounds hole, Bounds geometry)
    {
        var gCenter = geometry.center;
        var gExtents = geometry.extents;
        
        var hCenter = hole.center;
        var hExtents = hole.extents;

        for (int i = 0; i < 6; i++)
        {
            var center = geometry.center;
            var size = geometry.size;
            
            switch (i)
            {
                case 0:
                {
                    if (hCenter.x - hExtents.x < gCenter.x - gExtents.x) continue;
                    center.x = (hCenter.x - hExtents.x + gCenter.x - gExtents.x) / 2;
                    size.x = Vector3.Distance(new Vector3(hCenter.x + -hExtents.x, 0, 0), new Vector3(gCenter.x + -gExtents.x, 0, 0));
                    break;
                }
                case 1:
                {
                    if (hCenter.x + hExtents.x > gCenter.x + gExtents.x) continue;
                    center.x = (hCenter.x + hExtents.x + gCenter.x + gExtents.x) / 2;
                    size.x = Vector3.Distance(new Vector3(hCenter.x + hExtents.x, 0, 0), new Vector3(gCenter.x + gExtents.x, 0, 0));
                    break;
                }
                case 2:
                {
                    if (hCenter.y + hExtents.y > gCenter.y + gExtents.y) continue;
                    center.y = (hCenter.y + hExtents.y + gCenter.y + gExtents.y) / 2;
                    size.y = Vector3.Distance(new Vector3(0, hCenter.y + hExtents.y, 0), new Vector3(0, gCenter.y + gExtents.y, 0));
                    break;
                }
                case 3:
                {
                    if (hCenter.y - hExtents.y < gCenter.y - gExtents.y) continue;
                    center.y = (hCenter.y - hExtents.y + gCenter.y - gExtents.y) / 2;
                    size.y = Vector3.Distance(new Vector3(0, hCenter.y + -hExtents.y, 0), new Vector3(0, gCenter.y + -gExtents.y, 0));
                    break;
                }
                case 4:
                {
                    if (hCenter.z - hExtents.z < gCenter.z - gExtents.z) continue;
                    center.z = (hCenter.z - hExtents.z + gCenter.z - gExtents.z) / 2;
                    size.z = Vector3.Distance(new Vector3(0, 0, hCenter.z + -hExtents.z), new Vector3(0, 0, gCenter.z + -gExtents.z));
                    break;
                }
                case 5:
                {
                    if (hCenter.z + hExtents.z > gCenter.z + gExtents.z) continue;
                    center.z = (hCenter.z + hExtents.z + gCenter.z + gExtents.z) / 2;
                    size.z = Vector3.Distance(new Vector3(0, 0, hCenter.z + hExtents.z), new Vector3(0, 0, gCenter.z + gExtents.z));
                    break;
                }
            }
            var go = new GameObject();
            var col = go.AddComponent<BoxCollider>();

            col.center = center;
            col.size = size;

            go.transform.parent = transform;
        }
    }
}
