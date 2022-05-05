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
        //python���}�����|
        string path = @"C:\Users\B20_PC3\Desktop\DAN\Augmented-reality-in-Industrial-maintenance\Assets\Script\" + sArgName;
        string sArguments = path;

        //(�`�N:�Ϊ��ܻݭn�����ۤv��)�S���t�����ܶq���ܡA�i�H���ڳo�˼gpython.exe��������|
        //(�Ϊ��ܻݭn�����ۤv��) �p�G�t�F�A�����g"python.exe"�Y�i
        p.StartInfo.FileName = @"C:\Users\B20_PC3\AppData\Local\Programs\Python\Python39\python.exe";
        //p.StartInfo.FileName = @"C:\Program Files\Python35\python.exe";


        // sArguments��python�}�������| python�Ȫ��ǻ����ustrArr[]->teps->sigstr->sArguments
        //�bpython����sys.argv[ ]�ϥθӰѼ�
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