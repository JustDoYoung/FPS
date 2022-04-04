using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공격 로직(코루틴)
public class Weapon : MonoBehaviour
{
    public enum Type {  Melee, Range };
    public Type type;
    public int damage;
    public float rate; //공격속도
    public BoxCollider meleeArea; //공격범위
    public TrailRenderer trailEffect;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing"); //코루틴 정지 함수(실행 중에도 중지)
            StartCoroutine("Swing");
        }
    }
    IEnumerator Swing()
    {
        //yield return null; //결과를 전달하는 키워드, 1프레임 대기
        //yield break; 코루틴 탈출
        yield return new WaitForSeconds(0.1f); //0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    //Use() 메인루틴 => Swing() 서브루틴 => Use() 메인루틴 : 기존 함수실행.
    //Use() 메인루틴 + Swing() 코루틴 : 동시실행.
}


