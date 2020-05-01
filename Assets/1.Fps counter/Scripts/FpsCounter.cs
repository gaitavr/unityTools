using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField]
    private int _frameRange = 60;

    private int[] _fpsBuffer;
    private int _fpsBufferIndex;

    public int AverageFPS { get; private set; }
    public int HighestPFS { get; private set; }
    public int LowersFPS { get; private set; }

    private void Update()
    {
        if (_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
        {
            InitializeBuffer();
        }

        UpdateBuffer();
        CalculateFps();
    }

    private void InitializeBuffer()
    {
        if (_frameRange <= 0)
        {
            _frameRange = 1;
        }

        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _fpsBuffer[_fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (_fpsBufferIndex >= _frameRange)
        {
            _fpsBufferIndex = 0;
        }
    }

    private void CalculateFps()
    {
        int sum = 0;
        int lowest = int.MaxValue;
        int highest = 0;
        for (int i = 0; i < _frameRange; i++)
        {
            int fps = _fpsBuffer[i];
            sum += fps;
            if (fps > highest)
            {
                highest = fps;
            }

            if (fps < lowest)
            {
                lowest = fps;
            }
        }

        HighestPFS = highest;
        LowersFPS = lowest;
        AverageFPS = sum / _frameRange;
    }
}
