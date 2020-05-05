using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainThreadThrower : MonoBehaviour
{
    private float _timer;

    private const float TIME_BETWEEN_MESSAGES = 0.5f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > TIME_BETWEEN_MESSAGES)
        {
            _timer = 0;
            LogMain();
        }
    }

    private void LogMain()
    {
        var rnd = Random.Range(0, 4);
        switch (rnd)
        {
            case 0:
                Debug.Log(GetLongMessage(4000));
                //Debug.Log("Log from MAIN THREAD");
                break;
            case 1:
                Debug.LogWarning("Warning from MAIN THREAD");
                break;
            case 2:
                Debug.LogError("Error from MAIN THREAD");
                break;
            case 3:
                GameObject obj = null;
                obj.SetActive(false);
                break;
        }
    }

    private string GetLongMessage(int count)
    {
        var str = new StringBuilder();
        for (int i = 0; i < count; i++)
        {
            str.Append("A");
        }
        return str.ToString();
    }
}