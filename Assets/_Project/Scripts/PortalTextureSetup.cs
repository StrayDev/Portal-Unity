using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    [SerializeField] private Camera cameraA = default;
    [SerializeField] private Material cameraMatA = default;
    
    [SerializeField] private Camera cameraB = default;
    [SerializeField] private Material cameraMatB = default;

    private void Start()
    {
        AssignRenderTexture(cameraA, cameraMatA);
        AssignRenderTexture(cameraB, cameraMatB);
    }

    private void AssignRenderTexture(Camera cam, Material mat)
    {
        if (cam.targetTexture != null) cam.targetTexture.Release();
        
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mat.mainTexture = cam.targetTexture;
    }
}
