using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class CallPython : MonoBehaviour
{

    //vllt erstmal versuchen .py erstmal in der Python konsole zu öffnen

    string pythonPath = @"C:\Python310\python.exe";
    string mainPyPath = @"C:\Users\Mattes\Desktop\MotionRunners\MotionTracking\hand_gesture_detection.py";

    // Start is called before the first frame update
    void Start()
    {/*
        string arg = string.Format(@"C:\Users\Mattes\Desktop\MotionRunners\MotionTracking\main.py"); // Path to the Python code
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo(@"C:\Python310\python.exe", arg);
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.CreateNoWindow = true; // Hide the command line window
        p.StartInfo.RedirectStandardOutput = false;
        p.StartInfo.RedirectStandardError = false;
        Process processChild = Process.Start(p.StartInfo);
        print("runs python");
        */

       //     PythonEngine engine = new PythonEngine(); engine.LoadAssembly(Assembly.GetAssembly(typeof(GameObject))); engine.ExecuteFile("Test.py");

    }

    public static void run_cmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = cmd;//cmd is full path to python.exe
        start.Arguments = args;//args is path to .py file and any cmd line args
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                print(result);
            }
        }
    }
}
