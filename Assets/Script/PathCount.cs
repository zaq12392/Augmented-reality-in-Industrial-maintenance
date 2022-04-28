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
        users.Add(new PositionData() { X = prePos.x, Z = prePos.z }); //初始位置放進LIST
    }

    // Update is called once per frame
    void Update()
    {
        //有位移才紀錄位置
        endtime = Time.time;
        aftPos = gameObject.transform.position;
        if(aftPos != prePos)
        {
            users.Add(new PositionData() { X = aftPos.x, Z = aftPos.z });
            prePos = aftPos;
            print("list length" + users.Count);
        }
        
  
    }

    class PositionData
    {
        public float X { get; set; }
        public float Z { get; set; }

        public override string ToString()
        {
            return $"{this.X}, {this.Z}";
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
        users.Add(new PositionData() { X = Pos.x, Z = Pos.z });
        string filepath = @"C:\Users\3700X\Desktop\UserPath\Path.csv";  //檔案位置在桌面的UserPath裡面
        print("writeCSV");
        WriteToCSV(filepath, users);
        print("end game");
        
    }

  
    
}