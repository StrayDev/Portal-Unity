using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private float mouseSensitivity = 100f;
    
    private float m_XRotation = 0f;
    private bool freezeCamera = false;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) freezeCamera = !freezeCamera;
        if (freezeCamera) return;
        
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        m_XRotation -= mouseY;
        m_XRotation = Mathf.Clamp(m_XRotation, -90, 90);
        
        transform.localRotation = Quaternion.Euler(m_XRotation, 0, 0);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}
