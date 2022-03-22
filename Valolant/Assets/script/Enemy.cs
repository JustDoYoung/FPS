using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//�������� ���ؼ� �̵��ϰ� �ʹ�. 
//agent����� �̿��ؼ�
//FSM : ��ǥ�˻�, �⺻����(���-�ִϸ��̼� �̺�Ʈ), �̵�, ����
public class Enemy : MonoBehaviour
{
    //������
    enum State
    {
        SEARCH,
        MOVE,
        ATTACK
    }
    public Animator anim;
    float distance;
    State state; //���� ����

    GameObject target;
    NavMeshAgent agent;

    private void Awake()
    {
        Application.targetFrameRate = 40;
    }
    void Start()
    {
        state = State.SEARCH;
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.SEARCH:
                //���� ������°� ��ǥ�˻��̶�� �˻��� �ϰ� �ʹ�.
                UpdateSearch(); //alt + enter �޼ҵ� ����
                break;
            case State.MOVE:
                //�׷��� �ʰ� ���°� �̵��̶�� �̵��� �ϰ� �ʹ�.
                UpdateMove();
                break;
            case State.ATTACK:
                //�׷��� �ʰ� ���°� �����̶�� ���ݸ� �ϰ� �ʹ�.
                UpdateAttack();
                break;
        }
    }

    private void UpdateAttack()
    {
        //���� ���������� �Ÿ��� ���ݰ��ɰŸ��� �ƴ϶��?
        //�ٽ� �̵����·� �����ϰ� �ʹ�.
    }

    private void UpdateMove()
    {
        //agnet���� �������� ��� �˷��ְ� �ʹ�.(�� ã�� ����)
        agent.destination = target.transform.position;
        //���� ���������� �Ÿ��� ���ݰ��ɰŸ����?
        distance = Vector3.Distance(transform.position, target.transform.position); //�� ����Ʈ ���� �Ÿ�
        if (distance <= agent.stoppingDistance)
        {
            //���ݻ��·� �����ϰ� �ʹ�.
            state = State.ATTACK;
            anim.SetTrigger("Attack");
        }
    }

    private void UpdateSearch()
    {
        //�������� ã��ʹ�.
        target = GameObject.Find("Player");
        //���� �������� ã������ 
        if (target != null)
        {
            //�̵����·� �����ϰ� �ʹ�.
            state = State.MOVE;
            anim.SetTrigger("Move");
        }

    }
    internal void OnEnemyAttackHit()
    {
        distance = Vector3.Distance(transform.position, target.transform.position); //�� ����Ʈ ���� �Ÿ�
        
        //���� ���ݰ��� ������� Hit�� �ϰ� �ʹ�.
        if (distance <= agent.stoppingDistance)
        {
            print("Hit�� �ϰ� �ʹ�.");
            HitManager.instance.DoHitPlz();
        }
        else
        {
            //�̵����·� �����ϰ� �ʹ�.
            state = State.MOVE;
            anim.SetTrigger("Move");
            print("�̵����·� �����ϰ� �ʹ�.");
        }
    }

 

    internal void OnEnemyAttackFinished()
    {
        distance = Vector3.Distance(transform.position, target.transform.position); //�� ����Ʈ ���� �Ÿ�
        //���� ���ݰ��� ������ �ƴ϶��
        if (distance > agent.stoppingDistance)
        {
            //�̵����·� �����ϰ� �ʹ�.
            state = State.MOVE;
            anim.SetTrigger("Move");
            print("�̵����·� �����ϰ� �ʹ�.");
        }
    }
}
