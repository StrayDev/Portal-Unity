using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private BoxCollider hole = default;
    [SerializeField] private BoxCollider surface = default;

    private void Start()
    {
        Hole.CutHole(hole.bounds, surface.bounds, transform);
        surface.enabled = false;
        hole.enabled = false;
    }
}
