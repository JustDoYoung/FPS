using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//property : 함수인데 변수처럼 사용함.
//delegate : 대리자, 함수를 담는 변수

public class Test2 : MonoBehaviour
{
    public Test cube;
    void OnMoveRightComplete()
    {
        cube.Move(Vector3.up, OnMoveUpComplete);
    }
    void OnMoveUpComplete()
    {
        cube.Move(Vector3.left, OnMoveLeftComplete);
    }
    void OnMoveLeftComplete()
    {
        cube.Move(Vector3.down, OnMoveDownComplete);
    }
    void OnMoveDownComplete()
    {
        cube.Move(Vector3.right, OnMoveRightComplete);
    }

    private void Start()
    {
        //큐브에게 이동을 요청하고 싶은데
        //오른쪽으로 이동이 끝나면 위로 요청하고
        //위로 이동이 끝나면 왼쪽으로 요청하고
        //왼쪽 이동이 끝나면 아래로 요청하고
        cube.Move(Vector3.right, () =>
        {
            cube.Move(Vector3.up, () =>
            {
                cube.Move(Vector3.left, () =>
                {
                    cube.Move(Vector3.down, () =>
                    {
                        OnComplete();
                    });
                });
            });
        });
    }

    private void OnComplete()
    {
        Start();
    }

    void Update()
    {

    }
}
