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
        SEARCH,
        MOVE,
        ATTACK
    }
    public Animator anim;
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
        state = State.SEARCH;
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.SEARCH:
                //만약 현재상태가 목표검색이라면 검색만 하고 싶다.
                UpdateSearch(); //alt + enter 메소드 생성
                break;
            case State.MOVE:
                //그렇지 않고 상태가 이동이라면 이동만 하고 싶다.
                UpdateMove();
                break;
            case State.ATTACK:
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
            state = State.ATTACK;
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
            state = State.MOVE;
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
            state = State.MOVE;
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
            state = State.MOVE;
            anim.SetTrigger("Move");
            print("이동상태로 전이하고 싶다.");
        }
    }
}
