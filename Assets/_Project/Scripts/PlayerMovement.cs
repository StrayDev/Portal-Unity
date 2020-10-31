using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = default;
    [SerializeField] private float speed = 10f;
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var move = transform.right * x + transform.forward * z;
        var y = rb.velocity.y;
        
        rb.velocity = move * speed;
        rb.velocity += new Vector3(0,y,0);
    }
}
