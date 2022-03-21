using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //KeyCode.Alpha : 일자형 숫자배열, KeyCode.Keypad : 오른쪽 숫자배열
        {
            //Idle
            //anim.SetTrigger("Idle");
            //anim.Play("Idle", 0, 0); //fade가 사라져 모션이 바뀔 때 뚝뚝 끊기는 느낌이 있음.
            anim.CrossFade("Idle", 0.2f, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Walk
            anim.SetTrigger("Walk");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Attack
            anim.SetTrigger("Attack");
        }
    }
}
