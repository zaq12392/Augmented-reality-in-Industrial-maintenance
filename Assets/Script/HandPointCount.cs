using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Reflection;
using System.Linq;

public class HandPointCount : MonoBehaviour
{
    //���m��LIST
    List<PositionData>[] users = new List<PositionData>[21];
    Vector3[] prePos = new Vector3[21];
    Vector3[] aftPos = new Vector3[21];
    Vector3[] endPos = new Vector3[21];
    public GameObject[] point = new GameObject[21];
    public GameObject Hand;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
        // Update is called once per frame
    void Update()
    {
        //���UA�}�l���  ��bSTART�̭��L�k���`����
        if (Input.GetKeyUp(KeyCode.A))
        {
            findHand();
        }

        if (Input.GetKeyUp(KeyCode.S)) //�������
        {
            CancelInvoke();
        }


    }
    
    void findHand()
    {
        for (i = 0; i < 21; i++)
        {
            users[i] = new List<PositionData>() { };
            point[i] = Hand.transform.Find("point" + i).gameObject;
            prePos[i] = point[i].transform.position;
            users[i].Add(new PositionData() { X = prePos[i].x, Y = prePos[i].y, Z = prePos[i].z }); ; //��l��m��iLIST
        }
        //�C0.3��p�@��
        InvokeRepeating("Count", 0.1f, 0.1f);
    }
    void Count()
    {
        for (int i = 0; i < 21; i++)
        {
            aftPos[i] = point[i].transform.position;
            users[i].Add(new PositionData() { X = aftPos[i].x, Y = aftPos[i].y, Z = aftPos[i].z });
        }
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

    void WriteToCSV(string FilePath, List<PositionData> data) //�gCSV
    {
        StreamWriter file = new StreamWriter(FilePath);
        foreach (var item in data)
        {
            //file.WriteLineAsync(item.ToString());
            file.WriteLine(item.ToString());
        }
        file.Close();

    }

    private void OnApplicationQuit()  //�����ɧ��m��X��CSV
    {
        for (i = 0; i < 21; i++)
        {
            endPos[i] = point[i].transform.position; //���������ɯ�����m
            users[i].Add(new PositionData() { X = endPos[i].x, Y = endPos[i].y, Z = endPos[i].z });
            string filepath = @"E:\GitHub\Augmented-reality-in-Industrial-maintenance\Assets\HandPath\" + Hand.name + "point" + i + ".csv";  //�ɮצ�m�b�ୱ��UserPath�̭�
            WriteToCSV(filepath, users[i]);
        }
        
    }



}