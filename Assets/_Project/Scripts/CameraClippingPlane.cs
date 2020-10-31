using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClippingPlane : MonoBehaviour
{
    [SerializeField] private Camera portalCamera = default;
    [SerializeField] private Camera playerCamera = default;
    [SerializeField] private Transform portalSurface = default;

    
    public void SetNearPlane()
    {
        var dot = System.Math.Sign(Vector3.Dot(portalSurface.forward, portalSurface.position - portalCamera.transform.position));

        var cameraSpace = portalCamera.worldToCameraMatrix.MultiplyPoint(portalSurface.position);
        var cameraNormals = portalCamera.cameraToWorldMatrix.MultiplyVector(portalSurface.forward) * dot;
        var cameraDistance = -Vector3.Dot(cameraSpace, cameraNormals);

        var clippingPlaneInCameraSpace = new Vector4(cameraNormals.x, cameraNormals.y, cameraNormals.z, cameraDistance);

        portalCamera.projectionMatrix = playerCamera.CalculateObliqueMatrix(clippingPlaneInCameraSpace);
    }
}
