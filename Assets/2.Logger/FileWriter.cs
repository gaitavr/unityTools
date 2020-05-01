using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class FileWriter
{
    private string _folder;
    private string _filePath;
    private FileAppender _appender;
    private Thread _workingThread;
    private readonly ConcurrentQueue<LogMessage> _messages = new ConcurrentQueue<LogMessage>();
    private bool _disposing;

    private const string DateFormat = "yyyy-MM-dd";
    private const string LogTimeFormat = "{0:dd/MM/yyyy HH:mm:ss:ffff} [{1}]: {2}\r";

    public FileWriter(string folder)
    {
        _folder = folder;
        ManagePath();
        _workingThread = new Thread(StoreMessages)
        {
            IsBackground = true,
            Priority = ThreadPriority.BelowNormal
        };
        _workingThread.Start();
    }

    private void ManagePath()
    {
        _filePath = $"{_folder}/{DateTime.UtcNow.ToString(DateFormat)}.log";
    }

    public void Write(LogMessage message)
    {
        _messages.Enqueue(message);
    }

    private void StoreMessages()
    {
        while (!_disposing)
        {
            while (!_messages.IsEmpty)
            {
                try
                {
                    LogMessage message;
                    if (!_messages.TryPeek(out message))
                    {
                        Thread.Sleep(5);
                    }

                    if (_appender == null || _appender.FileName != _filePath)
                    {
                        _appender = new FileAppender(_filePath);
                    }

                    var messageToWrite = string.Format(LogTimeFormat, message.Time,
                        message.Type, message.Message);
                    if (_appender.Append(messageToWrite))
                    {
                        _messages.TryDequeue(out message);
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }
    }
}
