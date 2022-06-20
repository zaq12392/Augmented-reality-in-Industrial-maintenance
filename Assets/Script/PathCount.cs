using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Reflection;
using System.Linq;

public class PathCount : MonoBehaviour
{
    List<PositionData> users = new List<PositionData>() {};
    Vector3 prePos;
    Vector3 aftPos;
    float endtime;
    // Start is called before the first frame update
    void Start()
    {
        prePos = gameObject.transform.position;
        users.Add(new PositionData() { X = prePos.x, Y = aftPos.y, Z = prePos.z }); ; //初始位置放進LIST
        //每0.3秒計一次
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A)) //按下A開始紀錄
        {
            InvokeRepeating("Count", 0.1f, 0.1f);
        }

        if (Input.GetKeyUp(KeyCode.S)) //停止紀錄
        {
            CancelInvoke();
        }

    }

    void Count()
    {
        aftPos = gameObject.transform.position;
        users.Add(new PositionData() { X = aftPos.x, Y = aftPos.y, Z = aftPos.z });
    }

    class PositionData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"{this.X}, {this.Y}, {this.Z}";
        }
    }

    void WriteToCSV(string FilePath, List<PositionData> data) //寫CSV
    {
        StreamWriter file = new StreamWriter(FilePath);
        foreach (var item in data)
        {
            //file.WriteLineAsync(item.ToString());
            file.WriteLine(item.ToString());
        }
        file.Close();
        
    }

    private void OnApplicationQuit()  //結束時把位置輸出成CSV
    {
        Vector3 Pos = gameObject.transform.position; //紀錄結束時站的位置
        
        Debug.Log("endtime" + endtime.ToString());
        //string filename = "Path" + endtime.ToString() + ".csv";
        users.Add(new PositionData() { X = Pos.x, Y = Pos.y, Z = Pos.z });
        string filepath = @"E:\GitHub\Augmented-reality-in-Industrial-maintenance\Assets\HandPath\" + this.name + ".csv";  //檔案位置在桌面的UserPath裡面
        print("writeCSV");
        WriteToCSV(filepath, users);
        print("end game");
        
    }

  
    
}