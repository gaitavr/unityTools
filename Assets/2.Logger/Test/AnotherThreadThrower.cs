using System.Threading;
using UnityEngine;
using Random = System.Random;

public class AnotherThreadThrower : MonoBehaviour
{
    private Thread _logThread;
    private bool _isThreadAlive;
    private Random _random;

    private void Start()
    {
        _random = new Random();
        _isThreadAlive = true;
        _logThread = new Thread(SendLogs)
        {
            IsBackground = true,
        };
        _logThread.Start();
    }

    private void SendLogs()
    {
        while (_isThreadAlive)
        {
            LogThread();
            Thread.Sleep(150);
        }
    }

    private void LogThread()
    {
        var rnd = _random.Next(0, 4);
        switch (rnd)
        {
            case 0:
                Debug.Log("Log from ANOTHER THREAD");
                break;
            case 1:
                Debug.LogWarning("Warning from ANOTHER THREAD");
                break;
            case 2:
                Debug.LogError("Error from ANOTHER THREAD");
                break;
            case 3:
                int a = 100;
                int b = 0;
                int c = a / b;
                break;
        }
    }

    private void OnDestroy()
    {
        _isThreadAlive = false;
        _logThread.Abort();
    }
}
