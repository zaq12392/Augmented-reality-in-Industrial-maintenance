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

    public int cameraIndex = 0;   //camera的index
    public int deviceLengh = 0;   //目前有的camera數量

    WebCamTexture webCamTexture;
    public RawImage forDisplay;     //用ui的RawImage來顯示相機畫面

    public Mat mat;
    public Texture2D tex;
    private static DateTime lastSendTime = DateTime.Now;
    // Start is called before the first frame update
    void Start()
    {
        forDisplay = GameObject.Find("Canvas/AverMediaCamera").GetComponent<RawImage>();
        deviceLengh = WebCamTexture.devices.Length;   //取得裝置數量
        Debug.Log("camera數量 : " + deviceLengh);
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
                    Debug.Log("current camera : " + device.name);
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
            
            Utils.matToTexture2D(mat, tex);  //Mat to 2D
            //顯示
            forDisplay.texture = tex;
        }
    }
}
