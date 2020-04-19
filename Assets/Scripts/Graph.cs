using System;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private Transform _pointPrefab;
    [SerializeField, Range(10, 100)]
    private int _resolution = 10;
    [SerializeField]
    private GraphFunctionName _function;

    private List<Transform> _points = new List<Transform>();

    private void Update()
    {
        var t = Time.time;
        var function = _functions[(int) _function];
        var step = 2f / _resolution;
        var count = _resolution * _resolution;
        ManagePoints(count, step);
        if (_points.Count != count)
        {
            return;
        }
        for (int i = 0, z = 0; z < _resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < _resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                _points[i].localPosition = function(u, v, t);
            }
        }
    }

    private void ManagePoints(int count, float step)
    {
        if (_points.Count == count)
        {
            return;
        }

        if (_points.Count < count)
        {
            Vector3 scale = Vector3.one * step;
            for (int i = 0; i < count - _points.Count; i++)
            {
                AddPoint(scale);
            }
        }
        else
        {
            var pointsToRemove = new Transform[_points.Count - count];
            for (int i = 0; i < pointsToRemove.Length; i++)
            {
                pointsToRemove[i] = _points[_points.Count - 1 - i];
            }

            for (int i = 0; i < pointsToRemove.Length; i++)
            {
                RemovePoint(pointsToRemove[i]);
            }
        }
    }

    private void AddPoint(Vector3 scale)
    {
        Transform point = Instantiate(_pointPrefab);
        point.localScale = scale;
        point.SetParent(transform, false);
        _points.Add(point);
    }

    private void RemovePoint(Transform point)
    {
        Destroy(point.gameObject);
        _points.Remove(point);
    }

    private static GraphFunction[] _functions =
    {
        SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction,
        Ripple, Cylinder, Sphere, Torus
    };

    private const float PI = Mathf.PI;

    private static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.z = z;
        return p;
    }

    private static Vector3 MultiSineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.y += Mathf.Sin(2f * PI * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    private static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(PI * (x + t));
        p.y += Mathf.Sin(PI * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    private static Vector3 MultiSine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(PI * (x + z + t / 2f));
        p.y += Mathf.Sin(PI * (x + t));
        p.y += Mathf.Sin(2f * PI * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    private static Vector3 Ripple(float x, float z, float t)
    {
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z);
        p.x = x;
        p.y = Mathf.Sin(PI * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }

    private static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(PI * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(PI * u);
        p.y = v;
        p.z = r * Mathf.Cos(PI * u);
        return p;
    }

    private static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(PI * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(PI * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(PI * 0.5f * v);
        p.x = s * Mathf.Sin(PI * u);
        p.y = r * Mathf.Sin(PI * 0.5f * v);
        p.z = s * Mathf.Cos(PI * u);
        return p;
    }

    private static Vector3 Torus(float u, float v, float t)
    {
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin(PI * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(PI * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(PI * v) + r1;
        p.x = s * Mathf.Sin(PI * u);
        p.y = r2 * Mathf.Sin(PI * v);
        p.z = s * Mathf.Cos(PI * u);
        return p;
    }
}