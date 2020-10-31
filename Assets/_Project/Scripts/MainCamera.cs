using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCamera : MonoBehaviour
{
    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;

    }

    public void OnBeginCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        /*Debug.Log("Pre Cull");
        
        foreach (var portal in Portal.All)
        {
            //portal.Render();
        }*/
    }
}
