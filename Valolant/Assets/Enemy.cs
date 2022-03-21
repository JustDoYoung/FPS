using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//�������� ���ؼ� �̵��ϰ� �ʹ�. 
//agent����� �̿��ؼ�
public class Enemy : MonoBehaviour
{
    GameObject target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //agnet���� �������� ��� �˷��ְ� �ʹ�.(�� ã�� ����)
        agent.destination = target.transform.position;
    }
}
