using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using OpenCVForUnity;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using System;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ImgcodecsModule;
using System.Diagnostics;

public class GetCamera : MonoBehaviour
{

    public int cameraIndex = 0;   //camera��index
    public int deviceLengh = 0;   //�ثe����camera�ƶq

    WebCamTexture webCamTexture;
    public RawImage forDisplay;     //��ui��RawImage����ܬ۾��e��

    private Mat mat;
    private Mat OutMat;
    private Texture2D tex;
    private Texture2D OutTex;
    private static DateTime lastSendTime = DateTime.Now;

    string sArguments = @"unitytest.py";  //python�ɪ��W��
    // Start is called before the first frame update
    void Start()
    {
        forDisplay = GameObject.Find("Canvas/AverMediaCamera").GetComponent<RawImage>();
        deviceLengh = WebCamTexture.devices.Length;   //���o�˸m�ƶq
        UnityEngine.Debug.Log("camera�ƶq : " + deviceLengh);
        string deviceName;
        for ( int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            deviceName = WebCamTexture.devices[i].name;
            UnityEngine.Debug.Log("Name" + i  + " " + deviceName);
        }
        getTargetCamera();
        tex = new Texture2D(webCamTexture.width, webCamTexture.height);
        OutTex = new Texture2D(webCamTexture.width, webCamTexture.height);
        mat = new Mat(tex.height, tex.width, CvType.CV_8UC4);
        OutMat = new Mat(tex.height, tex.width, CvType.CV_8UC4);
    }

    // Update is called once per frame
    void Update()
    {
       WarpingCamera();
    }


    public void getTargetCamera()   //�ά۾��W�r��۾�
    {
        if (WebCamTexture.devices.Length > 0)
        {
            for (int i = 0; i < WebCamTexture.devices.Length; i++)  
            {
                if (WebCamTexture.devices[i].name.Contains("Streamer")) //���W�r�t��("XXX")���۾�
                {
                    WebCamDevice device = WebCamTexture.devices[i];
                    webCamTexture = new WebCamTexture(device.name);
                    webCamTexture.Play();
                    UnityEngine.Debug.Log("current camera : " + device.name);
                }
            }
        }
    }

    public void WarpingCamera()
    {
        TimeSpan timeInterval = DateTime.Now - lastSendTime;     //�קK�e����s�ӧ�
        if (timeInterval.TotalMilliseconds > 250 && webCamTexture.didUpdateThisFrame)
        {
            //webCamTexture�� Texture2D
            tex.SetPixels32(webCamTexture.GetPixels32());  
            tex.Apply();
            Utils.texture2DToMat(tex, mat);  //2D to Mat
            //���
            //BGR to RGB
            Imgproc.cvtColor(mat, mat, Imgproc.COLOR_BGR2RGB);
            Imgcodecs.imwrite(@"C:\Users\B20_PC3\Desktop\DAN\Augmented-reality-in-Industrial-maintenance\Assets\Resources\Camera.jpg", mat);
            //RunPythonScript(sArguments, "-u");
            OutMat = Imgcodecs.imread(@"C:\Users\B20_PC3\Desktop\DAN\Augmented-reality-in-Industrial-maintenance\Assets\Resources\OutCamera.jpg", 1);
            Imgproc.cvtColor(OutMat, OutMat, Imgproc.COLOR_BGR2RGB);
            Utils.matToTexture2D(OutMat, OutTex);
            forDisplay.texture = tex;
        }
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
