using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ڰ� GŰ�� ������ ���忡�� ��ź�� ���� �� ��ġ�� ��ġ�ϰ� �ʹ�.
//��ź�� �չ����� ���� �������� �ϴ� ����(grenadeThrowPosition)���� ȸ����Ű�� �ʹ�.(�� ���� 50�� ����)
//
public class PlayerThrow : MonoBehaviour
{
    public GameObject grenadeFactory;
    public Transform grenadeThrowPosition;

    // Update is called once per frame
    void Update()
    {
        //����ڰ� GŰ�� ������
        if (Input.GetKeyDown(KeyCode.G))
        {
        //���忡�� ��ź�� ����
            GameObject grenade = Instantiate(grenadeFactory);
        //�� ��ġ�� ��ġ�ϰ� �ʹ�.
            grenade.transform.position = grenadeThrowPosition.position;
            //��ź�� �չ����� ���� �������� �ϴ� �������� ȸ����Ű�� �ʹ�.(�� ���� 45�� ����)
            //Vector3 dir = transform.forward + transform.up; //�� ���� 45�� ����
            //grenade.transform.foward = dir;
            //��ź�� �չ����� ���� �������� �ϴ� �������� ȸ����Ű�� �ʹ�.(�� ���� 50�� ����)
            //Quaternion q = grenadeThrowPosition.rotation * Quaternion.Euler(-50, 0, 0); //������ ȸ����ǥ * Quaternion.Euler(ȸ����)
            //grenade.transform.rotation = q;

            grenade.transform.forward = grenadeThrowPosition.forward; //�Ϲ����� FPS
        }
    }
}
