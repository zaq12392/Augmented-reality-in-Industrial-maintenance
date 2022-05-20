using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemControler : MonoBehaviour
{

    public GameObject PC;
    public GameObject WebCam;
    public GameObject WebCam_Mid;
    public GameObject Task;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))  // �u�����ȰT��
        {
            PC.SetActive(false);
            WebCam.SetActive(false);
            WebCam_Mid.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.W)) // ���ȰT�� + WebCam
        {
            WebCam_Mid.SetActive(true);
            WebCam.SetActive(false);
            PC.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.E)) // ���ȰT��  + PC
        {
            WebCam_Mid.SetActive(false);
            WebCam.SetActive(false);
            PC.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.R))  // ���ȰT�� + PC + WebCam
        {
            WebCam_Mid.SetActive(false);
            WebCam.SetActive(true);
            PC.SetActive(true);
        }
    }
}
