using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileAppender
{
    private readonly object _lockObject = new object();

    public string FileName { get; }

    public FileAppender(string fileName)
    {
        FileName = fileName;
    }

    public bool Append(string content)
    {
        try
        {
            lock (_lockObject)
            {
                using (FileStream fs = File.Open(FileName, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    var bytes = Encoding.UTF8.GetBytes(content);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
