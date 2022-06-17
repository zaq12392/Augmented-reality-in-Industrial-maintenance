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

        if (Input.GetKeyUp(KeyCode.A)) //���UA�}�l����
        {
            InvokeRepeating("timer1", 0.1f, 0.1f);
        }

        if (Input.GetKeyUp(KeyCode.S)) //�������
        {
            CancelInvoke();
        }
    }

    void timer1()  //�C0.1�����p��
    {
        _timer += 0.1f;
    }

    void WriteToCSV(string FilePath, float time) //�gCSV
    {
        StreamWriter file = new StreamWriter(FilePath);

        file.WriteLine(time.ToString());
        file.Close();

    }

    private void OnApplicationQuit()  //�����ɧ��m��X��CSV
    {
        string filepath = @"E:\GitHub\Augmented-reality-in-Industrial-maintenance\Assets\EyeTrackerTimer\TaskTime.csv";  //�ɮצ�m�b�ୱ��UserPath�̭�
        print("writeCSV");
        WriteToCSV(filepath, _timer);
        print("end game");

    }
}
