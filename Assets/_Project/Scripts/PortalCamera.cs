using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PortalCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera = default;
    [SerializeField] private Camera thisCamera = default;
    
    [SerializeField] private Transform thisPortal = default;
    [SerializeField] private Transform otherPortal = default;
    
    [SerializeField] private MeshRenderer screen = default;

    [SerializeField] private bool clippingEnabled = true;
    
    void OnEnable()
    {
        thisCamera.enabled = false;
        RenderPipelineManager.beginCameraRendering += CameraPositionAndRotation;
    }
    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= CameraPositionAndRotation;
    }

    private void CameraPositionAndRotation(ScriptableRenderContext context, Camera cam)
    {
        thisCamera.enabled = true;
        screen.enabled = false;
        
        var m = thisPortal.localToWorldMatrix * otherPortal.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        transform.RotateAround(thisPortal.position, thisPortal.up, 180f);

        if (clippingEnabled) SetNearPlane();

        UniversalRenderPipeline.RenderSingleCamera(context, thisCamera);

        thisCamera.enabled = false;
        screen.enabled = true;
    }
    
    public void SetNearPlane()
    {
        /*var dot = System.Math.Sign(Vector3.Dot(screen.transform.forward, screen.transform.position - thisCamera.transform.position));

        var cameraSpace = thisCamera.worldToCameraMatrix.MultiplyPoint(screen.transform.position);
        var cameraNormals = thisCamera.cameraToWorldMatrix.MultiplyVector(screen.transform.forward) * dot;
        var cameraDistance = -Vector3.Dot(cameraSpace, cameraNormals);

        var clippingPlaneInCameraSpace = new Vector4(cameraNormals.x, cameraNormals.y, cameraNormals.z, cameraDistance);
        
        thisCamera.projectionMatrix = playerCamera.CalculateObliqueMatrix(clippingPlaneInCameraSpace);*/

        var dist = Vector3.Distance(thisCamera.transform.position, thisPortal.position);
        thisCamera.nearClipPlane = dist;
    }
}
