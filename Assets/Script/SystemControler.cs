using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemControler : MonoBehaviour
{

    public GameObject PC;
    public GameObject WebCam;
    public GameObject WebCam_Mid;
    public GameObject[] Task;
    public GameObject EyeTracker;
    public int taskLength = 7;
    private int nowTask;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < taskLength; i++)
        {
            Task[i] = GameObject.Find("Task/Task" + (i+1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))  // 只有任務訊息
        {
            PC.SetActive(false);
            WebCam.SetActive(false);
            WebCam_Mid.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.W)) // 任務訊息 + WebCam
        {
            WebCam_Mid.SetActive(true);
            WebCam.SetActive(false);
            PC.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.E)) // 任務訊息  + PC
        {
            WebCam_Mid.SetActive(false);
            WebCam.SetActive(false);
            PC.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Keypad1))  //任務1
        {
            nowTask = 0;
            for(int i = 0; i < taskLength; i++)
            {
                if(i != nowTask)
                {
                    Task[i].SetActive(false);
                }
                Task[nowTask].SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad2)) //任務2
        {
            nowTask = 1;
            for (int i = 0; i < taskLength; i++)
            {
                if (i != nowTask)
                {
                    Task[i].SetActive(false);
                }
                Task[nowTask].SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad3)) //任務3
        {
            nowTask = 2;
            for (int i = 0; i < taskLength; i++)
            {
                if (i != nowTask)
                {
                    Task[i].SetActive(false);
                }
                Task[nowTask].SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad4))  //任務4
        {
            nowTask = 3;
            for (int i = 0; i < taskLength; i++)
            {
                if (i != nowTask)
                {
                    Task[i].SetActive(false);
                }
                Task[nowTask].SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad5)) //任務5
        {
            nowTask = 4;
            for (int i = 0; i < taskLength; i++)
            {
                if (i != nowTask)
                {
                    Task[i].SetActive(false);
                }
                Task[nowTask].SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad6))  //任務6
        {
            nowTask = 5;
            for (int i = 0; i < taskLength; i++)
            {
                if (i != nowTask)
                {
                    Task[i].SetActive(false);
                }
                Task[nowTask].SetActive(true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Keypad7))  //任務7
        {
            nowTask = 6;
            for (int i = 0; i < taskLength; i++)
            {
                if (i != nowTask)
                {
                    Task[i].SetActive(false);
                }
                Task[nowTask].SetActive(true);
            }
        }
    }
}
