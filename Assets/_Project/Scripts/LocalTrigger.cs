using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.layer = 8;
        Debug.Log("ENTER");
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 0;
        Debug.Log("EXIT");
    }
}
