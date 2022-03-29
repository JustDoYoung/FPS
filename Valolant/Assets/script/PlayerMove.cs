using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//점프를 뛰고싶다.(power, velocity, gravity)
public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public float speed = 5;
    public float jumpPower = 10;
    public float gravity = -9.81f;
    float yVelocity;
    int jumpCount; //다중 점프 뛰고 싶다.
    public int maxJumpCount = 2;
    private void Awake()
    {
        Application.targetFrameRate = 40;
    }
    void Update()
    {
        
        //if ((cc.collisionFlags & CollisionFlags.Below) != 0) //비트마스크 : Above나 Side는 따로 함수가 없기 때문에 비트마스크로 써야함.
        if (cc.isGrounded)
        {
            //땅에 서 있다면 점프 횟수를 0으로 초기화하고 싶다.
            jumpCount = 0;
        }
        else {
            //만약 공중이라면
            //1. y속도는 중력을 계속 받아야 한다.
            yVelocity += gravity * Time.deltaTime;
        }
        //yVelocity를 0으로 하고 싶다.

        //2. 만약 점프횟수가 최대점프회수보다 작고 점프키를 누르면 y속도를 jumpPower로 대입하고 싶다.
        if ((jumpCount < maxJumpCount) && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            jumpCount++;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        //방향축을 카메라를 기준으로 dir를 변경하고 싶다.***
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize(); //원본의 크기를 1로 cf)dir.normalized : 복사본을 만들어서 1로 바꾼 다음 반환.
        Vector3 velocity = dir * speed;
        
        //3. 이동방향의 y속성에 y속도를 대입하고 싶다.
        velocity.y = yVelocity;

        //transform.position += dir * speed * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime); //갈 수 있는 만큼만 가고 멈춘다.
    }

    //점프 이해
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 from = transform.position;
    //    Vector3 to = transform.position + Vector3.up * yVelocity;
    //    Gizmos.DrawLine(from, to);

    //}
}
