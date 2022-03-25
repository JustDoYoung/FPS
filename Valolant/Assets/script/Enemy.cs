using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//목적지를 향해서 이동하고 싶다. 
//agent기능을 이용해서
//FSM : 목표검색, 기본동작(대기-애니메이션 이벤트), 이동, 공격
public class Enemy : MonoBehaviour
{
    //열거형
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
    State state; //현재 상태

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
        //FSM을 구현할 때 switch문을 주로 사용한다.
        switch (state)
        {
            case State.Idle:
                //만약 현재상태가 목표검색이라면 검색만 하고 싶다.
                UpdateSearch(); //alt + enter 메소드 생성
                break;
            case State.Move:
                //그렇지 않고 상태가 이동이라면 이동만 하고 싶다.
                UpdateMove();
                break;
            case State.Attack:
                //그렇지 않고 상태가 공격이라면 공격만 하고 싶다.
                UpdateAttack();
                break;
        }
    }

    private void UpdateAttack()
    {
        //만약 목적지와의 거리가 공격가능거리가 아니라면?
        //다시 이동상태로 전이하고 싶다.
    }

    private void UpdateMove()
    {
        //agnet에게 목적지를 계속 알려주고 싶다.(길 찾기 실행)
        agent.destination = target.transform.position;
        //만약 목적지와의 거리가 공격가능거리라면?
        distance = Vector3.Distance(transform.position, target.transform.position); //두 포인트 간의 거리
        if (distance <= agent.stoppingDistance)
        {
            //공격상태로 전이하고 싶다.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    private void UpdateSearch()
    {
        //목적지를 찾고싶다.
        target = GameObject.Find("Player");
        //만약 목적지를 찾았으면 
        if (target != null)
        {
            //이동상태로 전이하고 싶다.
            state = State.Move;
            anim.SetTrigger("Move");
        }

    }
    internal void OnEnemyAttackHit()
    {
        distance = Vector3.Distance(transform.position, target.transform.position); //두 포인트 간의 거리
        
        //만약 공격가능 범위라면 Hit를 하고 싶다.
        if (distance <= agent.stoppingDistance)
        {
            print("Hit를 하고 싶다.");
            HitManager.instance.DoHitPlz();
        }
        else
        {
            //이동상태로 전이하고 싶다.
            state = State.Move;
            anim.SetTrigger("Move");
            print("이동상태로 전이하고 싶다.");
        }
    }

 

    internal void OnEnemyAttackFinished()
    {
        distance = Vector3.Distance(transform.position, target.transform.position); //두 포인트 간의 거리
        //만약 공격가능 범위가 아니라면
        if (distance > agent.stoppingDistance)
        {
            //이동상태로 전이하고 싶다.
            state = State.Move;
            anim.SetTrigger("Move");
            print("이동상태로 전이하고 싶다.");
        }
    }
    /// <summary>
    /// Player -> Enemy를 공격함.
    /// </summary>
    bool isdead;
    public void TryDamage(int damageValue)
    {
        //체력이 이미 0이하라면 함수를 바로 종료하고 싶다.
        //if (enemyHP.HP <= 0)
        //{
        //    return;
        //}
        if (isdead)
        {
            return;
        }
        //enemyHP를 싱글톤으로 사용하면 안 되는 이유 : 싱글톤은 씬에서 단 하나의 오브젝트에 컴포넌트가 적용될 때만 사용. enemy가 여러 개 생길 경우 제일 마지막 오브젝트에만 싱글톤 값이 적용되기 때문에.
        enemyHP.HP -= damageValue;
        //데미지를 입었을 때 추적기능을 멈추고 싶다.
        agent.isStopped = true;

        if (enemyHP.HP <= 0)
        {
            isdead = true;
            //죽음
            //애니메이션 이벤트
            state = State.Death;
            anim.SetTrigger("Death");
            //GetComponent<Collider>().enabled = false; //콜라이더를 무효화시켜 투명인간 취급
        }
        else
        {
            //휘청
            state = State.React;
            anim.SetTrigger("React");
        }
    }
    internal void OnEnemyReactFinished()
    {
        //리액션 애니메이션이 종료되는 순간 이동상태로 전이하고 싶다.
        state = State.Move;
        anim.SetTrigger("Move");
        //리액션 애니메이션이 종료되고 나서는 다시 추적기능을 활성화시키고 싶다.
        agent.isStopped = false;
    }

    internal void OnEnemyDeathFinished()
    {
        //죽음 애니메이션이 종료되는 순간 자기자신을 파괴하고 싶다.
        Destroy(gameObject);
    }
}
