using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;  //眼動儀的unsing
using System.IO;

public class EyeTrackerTimer : MonoBehaviour, IGazeFocusable
{
    public float _timer = 0;
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
        _timer += 0.1f;
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
    void WriteToCSV(string FilePath , float time) //寫CSV
    {
        StreamWriter file = new StreamWriter(FilePath);
        
        file.WriteLine(time.ToString());
        file.Close();

    }

    private void OnApplicationQuit()  //結束時把位置輸出成CSV
    {
        string filepath = @"E:\GitHub\Augmented-reality-in-Industrial-maintenance\Assets\EyeTrackerTimer\" + this.name + ".csv";  //檔案位置在桌面的UserPath裡面
        print("writeCSV");
        WriteToCSV(filepath, _timer);
        print("end game");

    }

}
