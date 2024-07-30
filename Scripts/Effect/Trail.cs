using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }
    public void OnTrail(){
        trailRenderer.emitting = true;
    }
    public void OffTrail(){
        trailRenderer.emitting = false;
    }
}
