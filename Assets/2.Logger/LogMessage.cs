using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LogMessage
{
    public LogType Type { get; set; }
    public DateTime Time { get; set; }
    public string Message { get; set; }

    public LogMessage(LogType type, string message)
    {
        Type = type;
        Message = message;
        Time = DateTime.UtcNow;
    }
}
