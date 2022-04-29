using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using OpenCVForUnity;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using System;

public class GetCamera : MonoBehaviour
{

    public int cameraIndex = 0;   //camera��index
    public int deviceLengh = 0;   //�ثe����camera�ƶq

    WebCamTexture webCamTexture;
    public RawImage forDisplay;     //��ui��RawImage����ܬ۾��e��

    public Mat mat;
    public Texture2D tex;
    private static DateTime lastSendTime = DateTime.Now;
    // Start is called before the first frame update
    void Start()
    {
        forDisplay = GameObject.Find("Canvas/AverMediaCamera").GetComponent<RawImage>();
        deviceLengh = WebCamTexture.devices.Length;   //���o�˸m�ƶq
        Debug.Log("camera�ƶq : " + deviceLengh);
        string deviceName;
        for ( int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            deviceName = WebCamTexture.devices[i].name;
            Debug.Log("Name" + i  + " " + deviceName);
        }
        getTargetCamera();
        tex = new Texture2D(webCamTexture.width, webCamTexture.height);
        mat = new Mat(tex.height, tex.width, CvType.CV_8UC4);
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
                    Debug.Log("current camera : " + device.name);
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
            
            Utils.matToTexture2D(mat, tex);  //Mat to 2D
            //���
            forDisplay.texture = tex;
        }
    }
}
