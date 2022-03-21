using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �ٰ�ʹ�.(power, velocity, gravity)
public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public float speed = 5;
    public float jumpPower = 10;
    public float gravity = -9.81f;
    float yVelocity;
    int jumpCount; //���� ���� �ٰ� �ʹ�.
    public int maxJumpCount = 2;
    // Update is called once per frame
    void Update()
    {
        
        //if ((cc.collisionFlags & CollisionFlags.Below) != 0) //��Ʈ����ũ : Above�� Side�� ���� �Լ��� ���� ������ ��Ʈ����ũ�� �����.
        if (cc.isGrounded)
        {
            //���� �� �ִٸ� ���� Ƚ���� 0���� �ʱ�ȭ�ϰ� �ʹ�.
            jumpCount = 0;
        }
        else {
            //���� �����̶��
            //1. y�ӵ��� �߷��� ��� �޾ƾ� �Ѵ�.
            yVelocity += gravity * Time.deltaTime;
        }
        //yVelocity�� 0���� �ϰ� �ʹ�.

        //2. ���� ����Ƚ���� �ִ�����ȸ������ �۰� ����Ű�� ������ y�ӵ��� jumpPower�� �����ϰ� �ʹ�.
        if ((jumpCount < maxJumpCount) && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            jumpCount++;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        //�������� ī�޶� �������� �����ϰ� �ʹ�.***
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize(); //������ ũ�⸦ 1�� cf)dir.normalized : ���纻�� ���� 1�� �ٲ� ���� ��ȯ.
        Vector3 velocity = dir * speed;
        
        
        //3. �̵������� y�Ӽ��� y�ӵ��� �����ϰ� �ʹ�.
        velocity.y = yVelocity;

        //transform.position += dir * speed * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime); //�� �� �ִ� ��ŭ�� ���� �����.
    }

    //���� ����
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 from = transform.position;
    //    Vector3 to = transform.position + Vector3.up * yVelocity;
    //    Gizmos.DrawLine(from, to);

    //}
}
