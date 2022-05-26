using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireControl : MonoBehaviour
{
    public GameObject Itself;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Itself.SetActive(false); //碰到就消失
    }
}
