using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RendererSetup(List<Vector3> posses)
    {
        lineRenderer.positionCount = posses.Count;

        Vector3[] possesArr = posses.ToArray();
        
        for(int i = 0; i < possesArr.Length; i++)
        {
            lineRenderer.SetPosition(i, possesArr[i]);
        }
    }
}
