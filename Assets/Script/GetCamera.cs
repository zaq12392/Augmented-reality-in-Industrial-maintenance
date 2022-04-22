using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GetCamera : MonoBehaviour
{

    public int cameraIndex = 0;   //camera��index
    public int deviceLengh = 0;   //�ثe����camera�ƶq

    WebCamTexture webCamTexture;
    public RawImage forDisplay;     //��ui��RawImage����ܬ۾��e��

    // Start is called before the first frame update
    void Start()
    {
        deviceLengh = WebCamTexture.devices.Length;   //���o�˸m�ƶq
        Debug.Log("camera�ƶq : " + deviceLengh);
        getTargetCamera();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //�}�Ҭ۾�
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
    //���ά۾�
    private void StopCamera()
    {
        forDisplay.texture = null;
        webCamTexture.Stop();
        webCamTexture = null;
    }
    //�������P�۾�
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

    public void getTargetCamera()   //�ά۾��W�r��۾�
    {
        if (WebCamTexture.devices.Length > 0)
        {
            for (int i = 0; i < WebCamTexture.devices.Length; i++)  
            {
                if (WebCamTexture.devices[i].name.Contains("Streamer")) //���W�r�t��("Streamer")���۾�
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
