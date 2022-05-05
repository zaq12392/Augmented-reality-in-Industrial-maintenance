using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;



public class LoadPython : MonoBehaviour
{
    string sArguments = @"CameraWarping.py";

    // Use this for initialization
    void Start()
    {

        RunPythonScript(sArguments, "-u");
    }

    // Update is called once per frame
    void Update()
    {
        RunPythonScript(sArguments, "-u");
    }

    public static void RunPythonScript(string sArgName, string args = "")
    {
        Process p = new Process();
        //python的腳本路徑
        string path = @"C:\Users\B20_PC3\Desktop\DAN\Augmented-reality-in-Industrial-maintenance\Assets\Script\" + sArgName;
        string sArguments = path;

        //(注意:用的話需要換成自己的)沒有配環境變量的話，可以像我這樣寫python.exe的絕對路徑
        //(用的話需要換成自己的) 如果配了，直接寫"python.exe"即可
        p.StartInfo.FileName = @"C:\Users\B20_PC3\AppData\Local\Programs\Python\Python39\python.exe";
        //p.StartInfo.FileName = @"C:\Program Files\Python35\python.exe";


        // sArguments為python腳本的路徑 python值的傳遞路線strArr[]->teps->sigstr->sArguments
        //在python中用sys.argv[ ]使用該參數
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.Arguments = sArguments;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();
        p.BeginOutputReadLine();
        p.OutputDataReceived += new DataReceivedEventHandler(Out_RecvData);
        Console.ReadLine();
        p.WaitForExit();
    }

    static void Out_RecvData(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            UnityEngine.Debug.Log(e.Data);

        }
    }

}