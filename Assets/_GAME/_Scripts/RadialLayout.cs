using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RadialLayout : MonoBehaviour
{
    public enum Distribution
    {
        Left,
        Center,
        Right
    }
    
    [SerializeField] private float _startAngle;
    [SerializeField] private float _endAngle;
    [SerializeField] private Vector2 _ellipseSize;
    [SerializeField] private Distribution _distribution;
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        int resolution = 20;
        float step = (_endAngle - _startAngle) / (float)resolution;
        float previousAngle = _startAngle;
        
        for (float angle = _startAngle + step; angle < _endAngle + step; angle += step)
        {
            float angleRad = angle * Mathf.Deg2Rad;
            float previousAngleRad = previousAngle * Mathf.Deg2Rad;
            
            var startPoint = new Vector3(Mathf.Cos(previousAngleRad) * _ellipseSize.x, Mathf.Sin(previousAngleRad) * _ellipseSize.y);
            var endPoint = new Vector3(Mathf.Cos(angleRad) * _ellipseSize.x, Mathf.Sin(angleRad) * _ellipseSize.y);
            
            Handles.DrawLine(transform.position + startPoint, transform.position + endPoint, 2);
            previousAngle = angle;
        }
    }
#endif
}
