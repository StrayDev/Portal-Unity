using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PortalCamera : MonoBehaviour
{
    [SerializeField] private Camera thisCamera = default;
    [SerializeField] private Transform thisPortal = default;
    [SerializeField] private MeshRenderer screen = default;
    
    private Camera playerCamera = default;
    private Transform otherPortal = default;
    private MeshRenderer otherPortalScreen = default;

    [SerializeField] private Shader shader = default;
    private Material material = default;
    
    void OnEnable()
    {
        playerCamera = Camera.main;
        otherPortal = Portal.Portals.Find(p => p != thisPortal.GetComponent<Portal>()).transform;
        otherPortalScreen = otherPortal.GetComponentInChildren<MeshRenderer>();
        material = new Material(shader);
        
        SetTargetTexture();
        
        thisCamera.enabled = false;
        RenderPipelineManager.beginCameraRendering += CameraPositionAndRotation;
    }

    private void SetTargetTexture()
    {
        if (thisCamera.targetTexture != null) thisCamera.targetTexture.Release();

        thisCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        otherPortalScreen.material = material;
        otherPortalScreen.material.mainTexture = thisCamera.targetTexture;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= CameraPositionAndRotation;
    }

    private void CameraPositionAndRotation(ScriptableRenderContext context, Camera cam)
    {
        if (otherPortal == null) return;
        
        thisCamera.enabled = true;
        screen.enabled = false;
        
        var m = thisPortal.localToWorldMatrix * otherPortal.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        transform.RotateAround(thisPortal.position, thisPortal.up, 180f);

        //SetNearPlane();

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
