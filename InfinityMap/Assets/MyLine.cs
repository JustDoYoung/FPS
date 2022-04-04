using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLine : MonoBehaviour
{
    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, transform.position); //시작위치
        lr.SetPosition(1, transform.position + transform.forward * 50); //끝 위치
    }
}
