using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public bool isChase;
    public BoxCollider meleeArea;
    public bool isAttack;
    public enum Type { A, B, C};
    public Type enemyType;

    Rigidbody rigid;
    BoxCollider boxCollider;

    Material mat;
    NavMeshAgent nav;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        ChaseStart();
    }

    private void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }

    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    void FreezeVelocity()
    {
       
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
       
    }

    //몬스터 공격
    void Targeting()
    {
        float targetRadius = 0;
        float targetRange = 0;
        switch (enemyType)
        {
            case Type.A:
                targetRadius = 1.5f;
                targetRange = 3f;
                break;
            case Type.B:
                targetRadius = 1f;
                targetRange = 10f;
                break;
            case Type.C:
                break;
        }

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if(rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false; //정지
        isAttack = true;
        anim.SetBool("isAttack", true);

        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;
            case Type.C:
                break;
        }
        
        isChase = true; 
        isAttack = false;
        anim.SetBool("isAttack", false);
    }
    private void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
    }
    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            isChase = false;
            nav.enabled = false; //사망 리액션을 유지하기 위해(nav가 켜져있으면 mesh 위에서만 움직임.)

            mat.color = Color.gray;
            gameObject.layer = 11;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            anim.SetTrigger("doDie");
            Destroy(gameObject, 3);
        }
    }
}
