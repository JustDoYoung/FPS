using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//만약 마우스 왼쪽 버튼을 누르면
//바라본 곳에 총알자국을 남기고 싶다.
//바라본 곳 : 메인 카메라
public class Gun : MonoBehaviour
{
    public GameObject bImpactFactory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
                GameObject bImpact = Instantiate(bImpactFactory);
                bImpact.transform.position = hitInfo.point; //실제 부딪힌 지점. point
                bImpact.transform.forward = hitInfo.normal;
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
