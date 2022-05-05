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

    public int cameraIndex = 0;   //camera的index
    public int deviceLengh = 0;   //目前有的camera數量

    WebCamTexture webCamTexture;
    public RawImage forDisplay;     //用ui的RawImage來顯示相機畫面

    private Mat mat;
    private Mat OutMat;
    private Texture2D tex;
    private Texture2D OutTex;
    private static DateTime lastSendTime = DateTime.Now;

    string sArguments = @"unitytest.py";  //python檔的名稱
    // Start is called before the first frame update
    void Start()
    {
        forDisplay = GameObject.Find("Canvas/AverMediaCamera").GetComponent<RawImage>();
        deviceLengh = WebCamTexture.devices.Length;   //取得裝置數量
        UnityEngine.Debug.Log("camera數量 : " + deviceLengh);
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


    public void getTargetCamera()   //用相機名字找相機
    {
        if (WebCamTexture.devices.Length > 0)
        {
            for (int i = 0; i < WebCamTexture.devices.Length; i++)  
            {
                if (WebCamTexture.devices[i].name.Contains("Streamer")) //找到名字含有("XXX")的相機
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
        TimeSpan timeInterval = DateTime.Now - lastSendTime;     //避免畫面更新太快
        if (timeInterval.TotalMilliseconds > 250 && webCamTexture.didUpdateThisFrame)
        {
            //webCamTexture轉 Texture2D
            tex.SetPixels32(webCamTexture.GetPixels32());  
            tex.Apply();
            Utils.texture2DToMat(tex, mat);  //2D to Mat
            //顯示
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
