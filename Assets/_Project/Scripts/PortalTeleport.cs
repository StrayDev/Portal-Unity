using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
  [SerializeField] private Transform player = default;
  [SerializeField] private Transform receivingPortal = default;

  private bool isOverlapping = false;

  private void Update()
  {
    if (!isOverlapping) return;

    var vectorToObject = player.position - transform.position;
    var dotProduct = Vector3.Dot(transform.up, vectorToObject);

    if ( true ) // dotProduct < 0f)
    {
      var rotationDifference = -Quaternion.Angle(transform.rotation, receivingPortal.rotation);
      rotationDifference += 180;
      player.Rotate(Vector3.up, rotationDifference);

      var positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * vectorToObject;
      player.position = receivingPortal.position + positionOffset;

      var rb = player.gameObject.GetComponent<Rigidbody>();
      var locVel = transform.InverseTransformDirection(rb.velocity);
      //locVel.z = MovSpeed;
      rb.velocity = transform.TransformDirection(locVel);
      
      isOverlapping = false;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Origin") isOverlapping = true;
  }
}
