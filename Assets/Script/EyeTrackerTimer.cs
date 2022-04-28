using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;  //眼動儀的unsing

public class EyeTrackerTimer : MonoBehaviour, IGazeFocusable
{
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void timer1()  //每0.1秒為單位計時
    {
        timer += 0.1f;
    }

    public void GazeFocusChanged(bool hasFocus)  //跟IGazeFocusable 搭配的方程式  可以偵測物件有沒有正在被注視
    {
        if (hasFocus)   //被注視
        {
            InvokeRepeating("timer1", 0.1f, 0.1f);  //計時器    InvokeRepeating(方法名,幾秒開始計,每幾秒計一次)
        } 
        else  //沒注視
        {
            CancelInvoke();
        }
    }

    
}
