using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���콺 ���� ��ư�� ������
//�ٶ� ���� �Ѿ��ڱ��� ����� �ʹ�.
//�ٶ� �� : ���� ī�޶�
public class Gun : MonoBehaviour
{
    public GameObject bImpactFactory;
    public GameObject bImpactForEnemyFactory;

    private void Awake()
    {
        Application.targetFrameRate = 40;
        //���Ӻ信�� Ŀ���� �� ���̰� �ϰ� �ʹ�
        Cursor.visible = false;
        //Ŀ���� �߾ӿ� �����ϰ� �ʹ�.
        Cursor.lockState = CursorLockMode.Locked;
        ////Ŀ���� ������ â ������ �� ������ �ϰ� �ʹ�.
        //Cursor.lockState = CursorLockMode.Confined;

        //���� �ʱ� ��ġ
        gunTargetPos = zoomOutPos.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateZoom();
        UpdateFire();
    }

    public float zoomInValue = 15;
    public float zoomOutValue = 60;
    float targetZoomvalue = 60;
    public Transform zoomInPos;
    public Transform zoomOutPos;
    public GameObject gunObject;
    Vector3 gunTargetPos;
    private void UpdateZoom()
    {
        //���� ���콺 ������ ��ư�� ������ ������
        if (Input.GetButton("Fire2"))
        {
            //ZoomIn(Ȯ�� : 15��ŭ)�� �ϰ� �ʹ�.
            //Camera.main.fieldOfView = zoomInValue;
            targetZoomvalue = zoomInValue;
            gunTargetPos = zoomInPos.localPosition;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            //�׷��� �ʰ� ���� ZoomOut(�������)�� �ϰ� �ʹ�.
            //targetZoomvalue = zoomOutValue;
            Camera.main.fieldOfView = zoomOutValue;
            targetZoomvalue = zoomOutValue;
            gunTargetPos = zoomOutPos.localPosition;
        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetZoomvalue, Time.deltaTime * 20); //�������� (����, ��, ���۰� �� ����), �ڷ����� flaot�� MathfLerp
        gunObject.transform.localPosition = Vector3.Lerp(gunObject.transform.localPosition, gunTargetPos, Time.deltaTime * 20); //�ڷ����� Vector3�� Vector3.Lerp
    }

    void UpdateFire()
    {
        //���� ���콺 ���� ��ư�� ������
        if (Input.GetButtonDown("Fire1"))
        {
            //�ٶ� ���� �Ѿ��ڱ��� ����� �ʹ�.
            //�ʿ��� �� : �ü�, �ٶ󺸴�, ���� ���� ����
            //���� ī�޶��� ��ġ���� ���� ī�޶��� �չ������� �ü��� ����� �ʹ�.(���������� : �� ������ ������� ����) - �ϳ��� ����(�ϱ�)
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //(������, ����)

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                print(hitInfo.transform.name);
                GameObject bImpact;
                bool isEnemy = hitInfo.collider.CompareTag("Enemy"); //collier : �浹�Ǵ� �ش� ������Ʈ
                //Enemy�� ���� �����ڱ��� �� ����� �������� ����
                //Enemy�� �ƴ� �� �����ڱ��� ����� �������� ����ϰ� �ʹ�.
                if (isEnemy)
                {
                    bImpact = Instantiate(bImpactForEnemyFactory);
                }
                else
                {
                    bImpact = Instantiate(bImpactFactory);
                }
                //���׿����ڷ� ���� �� �ִ�.
                //bImpact = isEnemy ? Instantiate(bImpactForEnemyFactory) : Instantiate(bImpactFactory);

                bImpact.transform.position = hitInfo.point; //���� �ε��� ����. point
                bImpact.transform.forward = hitInfo.normal;

                if (isEnemy)
                {
                    //���� ü���� 1 ���̰� �ʹ�.
                    //������ �ѿ� �¾Ҿ� ��� �˷��ְ� �ʹ�.
                    //������ ũ�⸦ �˷��ְ� �ʹ�.
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>(); //transform : Rigidbody�� �ִ� ������Ʈ
                    enemy.TryDamage(1);
                }
            }
            //Call by Value
            //int float bool struct
            //Call by Reference
            //class array
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = Camera.main.transform.position;
        Vector3 to = Camera.main.transform.position + Camera.main.transform.forward * 100;
        Gizmos.DrawLine(from, to);

    }
}