using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;  //���ʻ���unsing
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
    void timer1()  //�C0.1�����p��
    {
        _timer += 0.1f;
    }

    public void GazeFocusChanged(bool hasFocus)  //��IGazeFocusable �f�t����{��  �i�H�������󦳨S�����b�Q�`��
    {
        if (hasFocus)   //�Q�`��
        {
            InvokeRepeating("timer1", 0.1f, 0.1f);  //�p�ɾ�    InvokeRepeating(��k�W,�X��}�l�p,�C�X��p�@��)
        } 
        else  //�S�`��
        {
            CancelInvoke();
        }
    }
    void WriteToCSV(string FilePath , float time) //�gCSV
    {
        StreamWriter file = new StreamWriter(FilePath);
        
        file.WriteLine(time.ToString());
        file.Close();

    }

    private void OnApplicationQuit()  //�����ɧ��m��X��CSV
    {
        string filepath = @"E:\GitHub\Augmented-reality-in-Industrial-maintenance\Assets\EyeTrackerTimer\" + this.name + ".csv";  //�ɮצ�m�b�ୱ��UserPath�̭�
        print("writeCSV");
        WriteToCSV(filepath, _timer);
        print("end game");

    }

}
