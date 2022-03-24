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
