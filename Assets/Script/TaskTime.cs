using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TaskTime : MonoBehaviour
{

    public float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.A)) //按下A開始紀錄
        {
            InvokeRepeating("timer1", 0.1f, 0.1f);
        }

        if (Input.GetKeyUp(KeyCode.S)) //停止紀錄
        {
            CancelInvoke();
        }
    }

    void timer1()  //每0.1秒為單位計時
    {
        _timer += 0.1f;
    }

    void WriteToCSV(string FilePath, float time) //寫CSV
    {
        StreamWriter file = new StreamWriter(FilePath);

        file.WriteLine(time.ToString());
        file.Close();

    }

    private void OnApplicationQuit()  //結束時把位置輸出成CSV
    {
        string filepath = @"E:\GitHub\Augmented-reality-in-Industrial-maintenance\Assets\EyeTrackerTimer\TaskTime.csv";  //檔案位置在桌面的UserPath裡面
        print("writeCSV");
        WriteToCSV(filepath, _timer);
        print("end game");

    }
}
