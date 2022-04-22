using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GetCamera : MonoBehaviour
{

    public int cameraIndex = 0;   //camera的index
    public int deviceLengh = 0;   //目前有的camera數量

    WebCamTexture webCamTexture;
    public RawImage forDisplay;     //用ui的RawImage來顯示相機畫面

    // Start is called before the first frame update
    void Start()
    {
        deviceLengh = WebCamTexture.devices.Length;   //取得裝置數量
        Debug.Log("camera數量 : " + deviceLengh);
        getTargetCamera();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //開啟相機
    public void SwitchOnAndOff()
    {
        if (webCamTexture != null)
        {
            StopCamera();
        }
        else
        {
            WebCamDevice device = WebCamTexture.devices[cameraIndex];
            webCamTexture = new WebCamTexture(device.name);
            forDisplay.texture = webCamTexture;
            webCamTexture.Play();
            Debug.Log("current camera : " + device.name);
        }
    }
    //停用相機
    private void StopCamera()
    {
        forDisplay.texture = null;
        webCamTexture.Stop();
        webCamTexture = null;
    }
    //切換不同相機
    public void SwitchCamera()
    {
        if(WebCamTexture.devices.Length > 0)
        {
            cameraIndex += 1;
            cameraIndex %= WebCamTexture.devices.Length;

            if(webCamTexture != null)
            {
                StopCamera();
                SwitchOnAndOff();
            }
        }
    }

    public void getTargetCamera()   //用相機名字找相機
    {
        if (WebCamTexture.devices.Length > 0)
        {
            for (int i = 0; i < WebCamTexture.devices.Length; i++)  
            {
                if (WebCamTexture.devices[i].name.Contains("Streamer")) //找到名字含有("Streamer")的相機
                {
                    WebCamDevice device = WebCamTexture.devices[i];
                    webCamTexture = new WebCamTexture(device.name);
                    forDisplay.texture = webCamTexture;
                    webCamTexture.Play();
                    Debug.Log("current camera : " + device.name);
                }
            }
        }
    }
}
