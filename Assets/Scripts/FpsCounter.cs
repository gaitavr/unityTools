using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    private int[] _fpsBuffer;
    private int _currentBufferIndex;

    [SerializeField]
    private int _frameRange = 60;

    public int AverageFps { get; private set; }
    public int HighestFps { get; private set; }
    public int LowerFps { get; private set; }

    private void Update()
    {
        if (_fpsBuffer == null || _fpsBuffer.Length != _frameRange)
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
        _currentBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _fpsBuffer[_currentBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
        if (_currentBufferIndex >= _frameRange)
        {
            _currentBufferIndex = 0;
        }
    }

    private void CalculateFps()
    {
        var sum = 0;
        HighestFps = 0;
        LowerFps = int.MaxValue;
        for (int i = 0; i < _frameRange; i++)
        {
            int fps = _fpsBuffer[i];
            sum += fps;
            if (fps > HighestFps)
            {
                HighestFps = fps;
            }

            if (fps < LowerFps)
            {
                LowerFps = fps;
            }
        }

        AverageFps = sum / _frameRange;
    }
}
