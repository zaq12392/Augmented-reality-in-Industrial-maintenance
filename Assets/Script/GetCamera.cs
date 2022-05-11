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
using System.IO;
using System.Drawing;
using System.Reflection;


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
    public int FPS;
    string sArguments = @"CameraWarping.py";  //python�ɪ��W��
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = FPS;
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
            //Imgproc.cvtColor(mat, mat, Imgproc.COLOR_BGR2RGB);
            //Imgcodecs.imwrite(@"E:\Augmented-reality-in-Industrial-maintenance\Assets\Resources\Camera.jpg", mat);
            //RunPythonScript(sArguments, "-u");
            //OutMat = Imgcodecs.imread(@"E:\Augmented-reality-in-Industrial-maintenance\Assets\Resources\OutCamera.jpg", 1);
            //Imgproc.cvtColor(OutMat, OutMat, Imgproc.COLOR_BGR2RGB);
            //Utils.matToTexture2D(OutMat, OutTex);
            Mat output = new Mat(tex.height, tex.width, CvType.CV_8UC4);
            output = ImageWarping(mat);
            Utils.matToTexture2D(output, OutTex);
            forDisplay.texture = OutTex;
            
        }
    }

    public Mat ImageWarping(Mat srcImage)   //�v���ᦱ
    {
        Mat result = new Mat(tex.height, tex.width, CvType.CV_8UC4);   //���@�ӭn��X��Mat
        //��opencvUnity���覡�ŧi point
        //src �O��ܵe�������� src1 ���W src2 ���U src3 �k�U 
        OpenCVForUnity.CoreModule.Point src1 = new OpenCVForUnity.CoreModule.Point(0, 0);
        OpenCVForUnity.CoreModule.Point src2 = new OpenCVForUnity.CoreModule.Point(0, tex.height);
        OpenCVForUnity.CoreModule.Point src3 = new OpenCVForUnity.CoreModule.Point(tex.width, tex.height);
        OpenCVForUnity.CoreModule.Point[] src_P = { src1, src2, src3 };

        //dst �O�q�����I  dst1 ���W  dst2 ���U  dst3 �k�U
        OpenCVForUnity.CoreModule.Point dst1 = new OpenCVForUnity.CoreModule.Point(234, 34);
        OpenCVForUnity.CoreModule.Point dst2 = new OpenCVForUnity.CoreModule.Point(200, 428);
        OpenCVForUnity.CoreModule.Point dst3 = new OpenCVForUnity.CoreModule.Point(359, 438);
        OpenCVForUnity.CoreModule.Point[] dst_P = { dst1, dst2, dst3 };

        //getAffineTransform�n�YMatOfPoint2f���榡 �ҥH�n���誺OpencvUnity��Point�]�_�ӥ�i�h
        MatOfPoint2f src = new MatOfPoint2f(src_P);
        MatOfPoint2f dst = new MatOfPoint2f(dst_P);
        Mat affine_M = new Mat();
        affine_M = Imgproc.getAffineTransform(dst, src);
        OpenCVForUnity.CoreModule.Size size = new OpenCVForUnity.CoreModule.Size(tex.width, tex.height);
        Imgproc.warpAffine(srcImage, result, affine_M, size);
        Imgcodecs.imwrite(@"E:\Augmented-reality-in-Industrial-maintenance\Assets\Resources\srcimage.jpg", srcImage);
        Imgcodecs.imwrite(@"E:\Augmented-reality-in-Industrial-maintenance\Assets\Resources\warpedimage.jpg", result);
        return result;
    }

    public static void RunPythonScript(string sArgName, string args = "")
    {
        Process p = new Process();
        //python���}�����|
        string path = @"E:\Augmented-reality-in-Industrial-maintenance\Assets\Script\" + sArgName;
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
