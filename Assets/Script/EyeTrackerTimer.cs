using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;  //���ʻ���unsing

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
    void timer1()  //�C0.1�����p��
    {
        timer += 0.1f;
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

    
}
