using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//화면을 번쩍거리게 하고 싶다.
//ImageHit 오브젝트를 보였다가 0.1초 후에 안 보이게 하고 싶다.
public class HitManager : MonoBehaviour
{
    public static HitManager instance; //static : instance 변수는 class ScoreManager의 것! 객체의 것이 아님

    private void Awake()
    {
        HitManager.instance = this; //class로 만들어진 객체가 자기자신을 부를 때 : this
    }

    public GameObject imageHit;

    void Start()
    {
        imageHit.SetActive(false);
    }
    public void DoHitPlz()
    {
        StopCoroutine(IEDoHit());
        StartCoroutine(IEDoHit());
    }

    IEnumerator IEDoHit()
    {
        //보이게 한다.
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        //안 보이게 한다.
        imageHit.SetActive(false);
        yield return new WaitForSeconds(0.1f);
    }
}
