using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colour { Blue, Orange }

public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab = default;

    
    [Header("These can be left empty")]
    [SerializeField] private GameObject bluePortal = default;
    [SerializeField] private GameObject orangePortal = default;

    private void CreatePortal(Colour colour, Transform target)
    {
        var portal = colour == Colour.Blue ? bluePortal : orangePortal;
        if (portal != null) Destroy(portal);
        
        portal = Instantiate(portalPrefab);
        portal.transform.SetPositionAndRotation(target.position, target.rotation);
    }

    private void OnCenterClick()
    {
        //Clears all existing portals
        Destroy(bluePortal);
        Destroy(orangePortal);
    }

    public void OnPortalAction()
    {
        // check target location
        //if (!isValidLocation()) return;

        var contextPosition = new Vector3();
        var contextRotation = new Quaternion();
        var contextColour = Colour.Blue;
        
        var otherPortal = GetOverlappingPortal(contextPosition, contextColour);
        if (!otherPortal) Destroy(otherPortal);
        
        //CreatePortal(contextColour, contextPosition);
    }

    private GameObject GetOverlappingPortal(Vector3 position, Colour colour)
    {
        var otherPortal =  colour == Colour.Blue ? bluePortal : orangePortal;
        var distance = otherPortal.transform.position - position;
        
        //if (distance < amount) return otherPortal;
        return null;
    }
}
