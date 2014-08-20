using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class DataLogger {

    private static DataLogger instance = new DataLogger();
    private FileStream logFile;
    private StreamWriter logWriter;
    public static DataLogger Instance
    {
        get
        {
            return instance;
        }
    }
    private DataLogger()
    {
        string logPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        logPath += "\\GameStats.log";
        logFile = new FileStream(logPath, FileMode.Append);
        logWriter = new StreamWriter(logFile);
    }

    public void Log(string text)
    {
        logWriter.WriteLine(DateTime.Now.ToString("dd MMM yyyy HH:mm:ss.ff")  + " " + text);
        logWriter.Flush();
    }
}
