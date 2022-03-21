using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���콺 ���� ��ư�� ������
//�ٶ� ���� �Ѿ��ڱ��� ����� �ʹ�.
//�ٶ� �� : ���� ī�޶�
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
                GameObject bImpact = Instantiate(bImpactFactory);
                bImpact.transform.position = hitInfo.point; //���� �ε��� ����. point
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
