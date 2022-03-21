using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //1. ����ڰ� ���콺�� �����̸�
        float mx = Input.GetAxis("Mouse X"); //���콺 �������� ��ȭ��
        float my = Input.GetAxis("Mouse Y");

        rx += rotSpeed * my * Time.deltaTime; //��ȭ���� ���� ȸ������
        ry += rotSpeed * mx * Time.deltaTime;

        rx = Mathf.Clamp(rx, -70, 70); //(value, min, max) : value�� ���� min�� max ���̷� �����Ѵ�.

        //2. ī�޶� ȸ���ϰ� �ʹ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
