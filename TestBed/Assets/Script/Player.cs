using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem dust;
    float hAxis;
    float vAxis;
    public float speed = 5;
    bool isWalk;
    bool jumpDown;
    bool isJump;
    bool isDodge;
    bool itemDown;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    bool swapDown1;
    bool swapDown2;
    bool isSwap;
    bool fireDown;
    bool isFireReady = true;
    float fireDelay;
    int equipWeaponIndex = -1;
    bool isBorder;
    public int health = 10;
    bool isDamage;

    Vector3 moveVec;
    Vector3 dodgeVec;
    Rigidbody rigid;

    Animator anim;

    GameObject nearObject;
    Weapon equipWeapon;
    MeshRenderer[] mesh;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        mesh = GetComponentsInChildren<MeshRenderer>(); //��� �ڽ� ������Ʈ
    }

    void createDust()
    {
        dust.Play();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        Interaction();
        Swap();
        Attack();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        isWalk = Input.GetButton("Walk");
        jumpDown = Input.GetButtonDown("Jump");
        itemDown = Input.GetButtonDown("Interaction");
        swapDown1 = Input.GetButtonDown("Swap1");
        swapDown2 = Input.GetButtonDown("Swap2");
        fireDown = Input.GetButtonDown("Fire1");
    }

    void Move()
    {
        if (hAxis != 0 || vAxis != 0)
        {
            print(hAxis);
            print(vAxis);
            createDust();
        }
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
        {
            moveVec = dodgeVec;
        }

        if (isSwap || !isFireReady)
        {
            moveVec = Vector3.zero;
        }

        //���濡 ��ֹ��� ���� ���� ��ġ�̵�(= ��ֹ��� ������ �������� �ʰ� ȸ���� ����)
        if (!isBorder)
        {
            transform.position += moveVec * (isWalk ? 0.5f : 1f) * speed * Time.deltaTime;
        }


        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", isWalk);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
        Debug.DrawRay(transform.position, moveVec * 20, Color.red);
    }

    void Jump()
    {
        if (jumpDown && !isJump && moveVec == Vector3.zero && !isDodge)
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Dodge()
    {
        if (jumpDown && !isJump && moveVec != Vector3.zero && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
            anim.SetBool("isJump", false);
        }
    }

    void Interaction()
    {
        if (itemDown && nearObject != null && !isJump && !isDodge)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    void Swap()
    {
        //���ҹ�ư�� �����µ� �������� ���ų� �̹� �տ� ��� �ִ� ��� �׳� ���������ڴ�.
        if (swapDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) return;
        if (swapDown2 && (!hasWeapons[1] || equipWeaponIndex == 1)) return;


        int weaponIndex = -1;
        if (swapDown1) weaponIndex = 0;
        if (swapDown2) weaponIndex = 1;
        //���ҹ�ư�� ������ ��
        if ((swapDown1 || swapDown2) && !isJump && !isDodge && !isSwap)
        {
            //�տ� �鸰 ���Ⱑ ������ ��Ȱ��ȭ��Ų��.
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false); //epuipWeapon(Weapon.cs) ������Ʈ�� ��� �ִ� ������Ʈ��~
            }
            //�� ������ ������Ʈ�� �Ҵ����ش�.
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            //�� ���⸦ �տ� ��� �ִٴ� ���� ����Ѵ�.
            equipWeaponIndex = weaponIndex;
            //�� ������ ������Ʈ�� Ȱ��ȭ��Ų��.
            equipWeapon.gameObject.SetActive(true);
            //�� ����� ��ü�� �� �ִϸ��̼��� Ȱ��ȭ�Ѵ�.
            anim.SetTrigger("doSwap");
            //���� �߿� �̵����� �ʵ��� �Ѵ�.Move()�� ����.
            isSwap = true;
            //0.4�� �ڿ� �̵��� �� �ִ�.
            Invoke("SwapOut", 0.3f);
        }
    }
    void SwapOut()
    {
        isSwap = false;
    }
    void Attack()
    {
        if (equipWeapon == null) return; //��� �ִ� ���� ������ pass

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay; //Update���� ��� ���ư��鼭 false, true ����

        if (fireDown && isFireReady && !isDodge && !isSwap)
        {
            equipWeapon.Use(); //collider, effect on
            anim.SetTrigger("doSwing"); //���ݸ��on
            fireDelay = 0;
        }
    }

    private void FixedUpdate()
    {
        StopToWall();
    }
    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall")); //���濡 Wall�̶�� Layer�� ������ true�� ��ȯ.
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Heart:
                    health += item.value;
                    Destroy(other.gameObject);
                    break;
            }
        }
        else if (other.gameObject.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                StartCoroutine(OnDamage());
            }
        }
    }
    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach (MeshRenderer mesh in mesh)
        {
            mesh.material.color = Color.yellow;
        }
        yield return new WaitForSeconds(0.5f);
        isDamage = false;
        foreach (MeshRenderer mesh in mesh)
        {
            mesh.material.color = Color.white;
        }
    }
}
