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
        Idle,
        Move,
        Attack,
        Death,
        React
    }
    public Animator anim;
    EnemyHP enemyHP;

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
        state = State.Idle;
        agent = this.GetComponent<NavMeshAgent>();
        enemyHP = this.GetComponent<EnemyHP>();
    }

    // Update is called once per frame
    void Update()
    {
        //FSM�� ������ �� switch���� �ַ� ����Ѵ�.
        switch (state)
        {
            case State.Idle:
                //���� ������°� ��ǥ�˻��̶�� �˻��� �ϰ� �ʹ�.
                UpdateSearch(); //alt + enter �޼ҵ� ����
                break;
            case State.Move:
                //�׷��� �ʰ� ���°� �̵��̶�� �̵��� �ϰ� �ʹ�.
                UpdateMove();
                break;
            case State.Attack:
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
            state = State.Attack;
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
            state = State.Move;
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
            state = State.Move;
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
            state = State.Move;
            anim.SetTrigger("Move");
            print("�̵����·� �����ϰ� �ʹ�.");
        }
    }
    /// <summary>
    /// Player -> Enemy�� ������.
    /// </summary>
    bool isdead;
    public void TryDamage(int damageValue)
    {
        //ü���� �̹� 0���϶�� �Լ��� �ٷ� �����ϰ� �ʹ�.
        //if (enemyHP.HP <= 0)
        //{
        //    return;
        //}
        if (isdead)
        {
            return;
        }
        //enemyHP�� �̱������� ����ϸ� �� �Ǵ� ���� : �̱����� ������ �� �ϳ��� ������Ʈ�� ������Ʈ�� ����� ���� ���. enemy�� ���� �� ���� ��� ���� ������ ������Ʈ���� �̱��� ���� ����Ǳ� ������.
        enemyHP.HP -= damageValue;
        //�������� �Ծ��� �� ��������� ���߰� �ʹ�.
        agent.isStopped = true;

        if (enemyHP.HP <= 0)
        {
            isdead = true;
            //����
            //�ִϸ��̼� �̺�Ʈ
            state = State.Death;
            anim.SetTrigger("Death");
            //GetComponent<Collider>().enabled = false; //�ݶ��̴��� ��ȿȭ���� �����ΰ� ���
        }
        else
        {
            //��û
            state = State.React;
            anim.SetTrigger("React");
        }
    }
    internal void OnEnemyReactFinished()
    {
        //���׼� �ִϸ��̼��� ����Ǵ� ���� �̵����·� �����ϰ� �ʹ�.
        state = State.Move;
        anim.SetTrigger("Move");
        //���׼� �ִϸ��̼��� ����ǰ� ������ �ٽ� ��������� Ȱ��ȭ��Ű�� �ʹ�.
        agent.isStopped = false;
    }

    internal void OnEnemyDeathFinished()
    {
        //���� �ִϸ��̼��� ����Ǵ� ���� �ڱ��ڽ��� �ı��ϰ� �ʹ�.
        Destroy(gameObject);
    }
}
