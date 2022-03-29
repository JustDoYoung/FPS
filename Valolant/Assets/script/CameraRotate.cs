using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 200;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 40;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //1. 사용자가 마우스를 움직이면
        float mx = Input.GetAxis("Mouse X"); //마우스 움직임의 변화량
        float my = Input.GetAxis("Mouse Y");

        rx += rotSpeed * my * Time.deltaTime; //변화량에 따른 회전각도(마우스 감도)
        ry += rotSpeed * mx * Time.deltaTime;

        rx = Mathf.Clamp(rx, -70, 70); //(value, min, max) : value의 값은 min과 max 사이로 제한한다.

        //2. 카메라를 회전하고 싶다.
        transform.eulerAngles = new Vector3(-rx, ry, 0); //degree
    }
}
