using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//만약 마우스 왼쪽 버튼을 누르면
//바라본 곳에 총알자국을 남기고 싶다.
//바라본 곳 : 메인 카메라
public class Gun : MonoBehaviour
{
    public GameObject bImpactFactory;
    public GameObject bImpactForEnemyFactory;

    private void Awake()
    {
        Application.targetFrameRate = 40;
        //게임뷰에서 커서가 안 보이게 하고 싶다
        Cursor.visible = false;
        //커서를 중앙에 고정하고 싶다.
        Cursor.lockState = CursorLockMode.Locked;
        ////커서를 윈도우 창 밖으로 안 나가게 하고 싶다.
        //Cursor.lockState = CursorLockMode.Confined;

        //총의 초기 위치
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
        //만약 마우스 오른쪽 버튼을 누르고 있으면
        if (Input.GetButton("Fire2"))
        {
            //ZoomIn(확대 : 15만큼)을 하고 싶다.
            //Camera.main.fieldOfView = zoomInValue;
            targetZoomvalue = zoomInValue;
            gunTargetPos = zoomInPos.localPosition;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            //그렇지 않고 떼면 ZoomOut(원래대로)을 하고 싶다.
            //targetZoomvalue = zoomOutValue;
            Camera.main.fieldOfView = zoomOutValue;
            targetZoomvalue = zoomOutValue;
            gunTargetPos = zoomOutPos.localPosition;
        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetZoomvalue, Time.deltaTime * 20); //선형보간 (시작, 끝, 시작과 끝 사이), 자료형이 flaot라서 MathfLerp
        gunObject.transform.localPosition = Vector3.Lerp(gunObject.transform.localPosition, gunTargetPos, Time.deltaTime * 20); //자료형이 Vector3라서 Vector3.Lerp
    }

    void UpdateFire()
    {
        //만약 마우스 왼쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            //바라본 곳에 총알자국을 남기고 싶다.
            //필요한 것 : 시선, 바라보다, 닿은 곳의 정보
            //메인 카메라의 위치에서 메인 카메라의 앞방향으로 시선을 만들고 싶다.(반직선으로 : 한 쪽으로 뻗어나가는 직선) - 하나의 구문(암기)
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //(시작점, 방향)

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                print(hitInfo.transform.name);
                GameObject bImpact;
                bool isEnemy = hitInfo.collider.CompareTag("Enemy"); //collier : 충돌되는 해당 오브젝트
                //Enemy일 때는 구멍자국이 안 생기는 프리팹을 쓰고
                //Enemy가 아닐 땐 구멍자국이 생기는 프리팹을 사용하고 싶다.
                if (isEnemy)
                {
                    bImpact = Instantiate(bImpactForEnemyFactory);
                }
                else
                {
                    bImpact = Instantiate(bImpactFactory);
                }
                //삼항연산자로 줄일 수 있다.
                //bImpact = isEnemy ? Instantiate(bImpactForEnemyFactory) : Instantiate(bImpactFactory);

                bImpact.transform.position = hitInfo.point; //실제 부딪힌 지점. point
                bImpact.transform.forward = hitInfo.normal;

                if (isEnemy)
                {
                    //적의 체력을 1 줄이고 싶다.
                    //적에게 총에 맞았어 라고 알려주고 싶다.
                    //데미지 크기를 알려주고 싶다.
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>(); //transform : Rigidbody가 있는 오브젝트
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